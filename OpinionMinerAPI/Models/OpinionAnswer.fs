namespace OpinionMinerAPI.Models

open Newtonsoft.Json
open System
open System.Collections.Generic

[<CLIMutable>]
type OpinionAnswer = {
    Term : string
    Data : Dictionary<DateTime, (float * float)>
}
