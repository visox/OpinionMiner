// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open UrlSource
open System
open WebParser
open EmotionalTextEvaluator

type SearchConditions =
    { fromDate : Option<DateTime>
      toDate : Option<DateTime>
    }

let GetComplexOpinion (term, conditions: SearchConditions) =    
    UrlSource.GetGoogleLinks(term, conditions.fromDate, conditions.toDate)
    |> Seq.map(fun l -> 
        EmotionalTextEvaluator.EvaluateText(
            WebParser.LoadPage(l).pureText))
    |> Seq.average

[<EntryPoint>]
let main argv = 
    let x = GetComplexOpinion("erni consulting", {fromDate = Some(new DateTime(2015,2,1)); toDate = Some(new DateTime(2015,3,1))})
    0 // return an integer exit code
