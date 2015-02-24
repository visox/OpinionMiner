// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open System.IO;
open System;
open System.Collections.Generic;

module EmotionalTextEvaluator =

    let WordValuesFileName = "wordValue.txt"

    let WordValues = new SortedDictionary<string, float>()

    let InitWordValues =
        File.ReadAllLines(WordValuesFileName) 
        |> Seq.iter(fun l -> 
            let word = l.Split('\t').[0]
            let value = float (l.Split('\t').[1])        
            WordValues.Add(word, value))

    InitWordValues

    let EvaluateText (text: string) =
        text.ToLower().Split(' ')
        |> Seq.filter(fun w -> WordValues.ContainsKey(w))
        |> Seq.map(fun w -> WordValues.[w])
        |> Seq.average
