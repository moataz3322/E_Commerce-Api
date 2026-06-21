using System.Text.Json.Serialization;

namespace E_Commerce.Application.Common
{//Da 4akl al error al hyrg3
    //hya mogrd data bn2lha 3n tare2 al object da f msh m7tag ykon class
    //SEALED 34an msh h5ly 7aga twrs meno 
    public sealed record Error(string Code , string Discreption ,  ErrorType Type=ErrorType.Failure)
    {

        //hna ana bb3t 4akl al error 3mtn
        public static Error Failure(string code = "General Failure", string discreption = "GeneralFailure Error") => new (code, discreption,ErrorType.Failure);
        public static Error Validation(string code = "General Validation", string discreption = "General Validation Error") => new (code, discreption,ErrorType.Validation);
        public static Error Conflict(string code = "General Conflict", string discreption = "Conflict Validation Error") => new (code, discreption,ErrorType.Conflict);
        public static Error NotFound(string code = "NotFound", string discreption = "Resource NotFound") => new (code, discreption,ErrorType.NotFound);
        public static Error UnAuthurized(string code = "UnAuthurized", string discreption = "UnAuthurized To Accsess") => new (code, discreption,ErrorType.UnAuthurized);
        public static Error InvalidCredentails(string code = "InvalidCredentails", string discreption = "InvalidCredentails") => new (code, discreption,ErrorType.InvalidCredentails);

        

    }


    //De 34an y7wl al Enum bdl m yzhr Rakm fe al error yzhr al klam
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType { 
    Failure=0,
    Validation=1,
    NotFound=2,
    Conflict=3
            ,UnAuthurized=4,
    Forbbiden=5,
    InvalidCredentails=6
    
    }
}