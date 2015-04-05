module EmotionalTextEvaluator

open System.IO;
open System;
open System.Collections.Generic;

let private WordValuesFileName = "wordValue.txt"

let private WordValues = new SortedDictionary<string, float>()

let private InitWordValues =
    File.ReadAllLines(WordValuesFileName) 
    |> Seq.iter(fun l -> 
        let word = l.Split('\t').[0]
        let value = float (l.Split('\t').[1])        
        WordValues.Add(word, value))

    

let EvaluateText (text: string) : float =
    text.ToLower().Split(' ')
    |> Seq.filter(fun w -> WordValues.ContainsKey(w))
    |> Seq.map(fun w -> WordValues.[w])
    |> fun s -> 
        match s with
        | nonEmpty when  Seq.length(nonEmpty) > 0 -> nonEmpty
        | _ -> seq {yield 0.0}
    |> Seq.average

InitWordValues
