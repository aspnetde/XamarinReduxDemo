namespace XamarinReduxDemo.Store

type AppState =
    { Cities : City array
      Users : User array }

module InitialState = 
    let Create() =
        // In Production you probably don't want to populate your
        // initial state with demo data – but for the purpose of
        // this demo that's a good place to start.
        { Cities =
            [| { Id = CityId(1); Name = "München" };
               { Id = CityId(2); Name = "New York" };
               { Id = CityId(3); Name = "Tokyo" } |]
          Users = 
            [| { Id = UserId(1); Name = "Max" };
               { Id = UserId(2); Name = "Zoe" };
               { Id = UserId(3); Name = "Carl" } |] }
