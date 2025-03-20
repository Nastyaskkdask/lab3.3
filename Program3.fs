open System
open System.IO

let getFiles (directoryPath: string) : seq<string> =
    try
        Directory.EnumerateFiles(directoryPath) |> Seq.cache
    with
    | ex ->
        printfn "Ошибка: %s" ex.Message
        Seq.empty 

let compare (filePath1: string) (filePath2: string) : string =
    printfn "Сравнение: '%s' vs '%s'" filePath1[28..] filePath2[28..]
    if String.Compare(filePath1, filePath2) > 0 then
        filePath1
    else
        filePath2

let findLast (directoryPath: string) : string option =
    let files = getFiles directoryPath
    if Seq.isEmpty files then
        None  
    else
        Some (Seq.reduce compare files)

[<EntryPoint>]
let main argv =
    let defaultDirectory = "C:\Users\Сергей\Desktop\New "  

    let directoryPath =
        if argv.Length > 0 then
            argv.[0]

        else
            defaultDirectory

    if not (Directory.Exists(directoryPath)) then
        printfn "Каталога '%s' не существует." directoryPath
    else 
        printfn "Каталог: %s" directoryPath
    

    match findLast directoryPath with
    | Some filePath ->
        printfn "Последний по алфавиту файл: %s" filePath[28..]

    | None ->
        printfn "Каталог пуст."

    0


