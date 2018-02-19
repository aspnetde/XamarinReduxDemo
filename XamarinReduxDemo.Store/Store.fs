namespace XamarinReduxDemo.Store

open System

type SubscriptionType<'TSelection> = 
    | StateSubscription of Action<AppState>
    | SelectionSubscription of Func<AppState, 'TSelection> * Action<'TSelection>
    | SelectionAndStateSubscription of Func<AppState, 'TSelection> * Action<'TSelection, AppState>

type Subscription(unsubscribe:Subscription -> unit, callback:obj * AppState -> unit, selector:AppState -> obj) = 
    member __.Callback = callback
    member __.Selector = selector
    interface IDisposable with
        member this.Dispose() = unsubscribe this

type IStore = 
    abstract CurrentState : AppState
    abstract Subscribe : SubscriptionType<'TSelection> -> Subscription
    abstract Unsubscribe : Subscription -> unit
    abstract Dispatch : StoreAction -> unit

type Store(initialState:AppState, rootReducer:StoreAction -> AppState -> AppState) = 
    let subscriptions = new ResizeArray<Subscription>()
    let mutable currentState = initialState
    let monitor = Object()
    let unsubscribe subscription = lock monitor (fun () -> 
        subscriptions.Remove subscription |> ignore
    )

    interface IStore with
        member __.CurrentState = currentState
        
        member __.Subscribe<'T>(subscriptionType:SubscriptionType<'T>) = 
            let (callback:obj * AppState -> unit, selector:AppState -> obj) = 
                match subscriptionType with
                | StateSubscription(c) -> 
                    (fun (_, state) -> c.Invoke(state)), 
                    fun _ -> id :> obj
                | SelectionSubscription(s, c) -> 
                    (fun (selection, _) -> c.Invoke((selection :?> 'T))), 
                    fun state -> s.Invoke state :> obj
                | SelectionAndStateSubscription(s, c) -> 
                    (fun (selection, state) -> c.Invoke((selection :?> 'T), state)), 
                    fun state -> s.Invoke state :> obj
            
            let subscription = new Subscription(unsubscribe, callback, selector)
            
            lock monitor (fun () -> 
                subscriptions.Add subscription
            )

            subscription
        
        member __.Unsubscribe subscription = unsubscribe subscription

        member __.Dispatch action = 
            lock monitor (fun () -> 
                let newState = currentState |> rootReducer action
                let oldState = currentState

                currentState <- newState

                subscriptions |> Seq.iter (fun subscription -> 
                    let newSelection = subscription.Selector newState
                    let oldSelection = subscription.Selector oldState

                    if newSelection <> oldSelection then 
                        subscription.Callback(newSelection, newState)
                )
            )
