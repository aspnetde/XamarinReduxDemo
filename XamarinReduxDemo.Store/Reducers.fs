module XamarinReduxDemo.Store.RootReducer

let reduceAppState action state = 
    match action with
    | CityRemoved removedCity -> 
        { state with 
            Cities = 
                state.Cities 
                    |> Array.filter (fun c -> c.Id <> removedCity.Id) }
    | UserUpdated updatedUser -> 
        { state with 
            Users = 
                state.Users 
                    |> Array.map (fun u -> if u.Id = updatedUser.Id then updatedUser else u) }        

let Create() = reduceAppState
