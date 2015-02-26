namespace OpinionMinerAPI.Models

open Newtonsoft.Json
open System

[<CLIMutable>]
type OpinionRequest = {
    Id : int
    Created : DateTime
    Term : string
    ToDate : DateTime
    DayCycle : int
    Repeate : int
    PageCount : int
}
