module Observable

open System

let private guard f (e:IObservable<'Args>) =  
    { 
        new IObservable<'Args> with  
        member x.Subscribe(observer) =  
            let rm = e.Subscribe(observer) in f(); rm 
    } 

let ofSeq s = 
    let evt = new Event<_>()
    evt.Publish |> guard (fun o ->
        for n in s do evt.Trigger(n))
