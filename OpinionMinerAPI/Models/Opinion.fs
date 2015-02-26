namespace OpinionMinerAPI.Models

open Newtonsoft.Json
open System

[<CLIMutable>]
type Opinion = {
    Id : int
    OpinionRequestId : int
    Result : float
    Created : DateTime
    Term : string
    FromDate : DateTime
    ToDate : DateTime
    PageCount : int
}
