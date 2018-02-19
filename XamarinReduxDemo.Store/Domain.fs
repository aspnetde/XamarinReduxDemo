namespace XamarinReduxDemo.Store

type CityId = CityId of int
type City =
    { Id : CityId
      Name : string }

type UserId = UserId of int
type User =
    { Id : UserId
      Name : string }
