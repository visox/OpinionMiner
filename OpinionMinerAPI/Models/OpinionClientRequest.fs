namespace OpinionMinerAPI.Models

open Newtonsoft.Json
open System

[<CLIMutable>]
type OpinionClientRequest = {
    Term : string
    ToDate : DateTime
}
