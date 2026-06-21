using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Application.Common
{//Void Result
  public  class Result
    {


        //4ayl al set 34an ana al h3ml helper method leha 
        public bool IsSuccess { get;  }

        public IReadOnlyList<Error> Error { get;  }

        //3mlo protected 34an a3rf a3ml chaining
        protected Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Error = errors;
        }
        //array.empty de ana b3tlo array fady msh null
        #region Void Result 
        public static Result Ok() => new Result(true, Array.Empty<Error>());

        public static Result Fail(Error error) => new Result(false, new[] { error });
        public static Result Fail(IReadOnlyList<Error> error) => new Result(false, error); 
        #endregion
    }


    public class Result<TValue> : Result 
    {
        private readonly TValue _value;

        //fe 7alt al Success ana 3ndy data 
        // fe 7alt al fail al data de bnull m3nde4 aslun value 
        public TValue data => IsSuccess ? _value : throw new InvalidOperationException("Cant acces the value of faild Result");


        #region Ctors 3 fourms
        //fe 7alt al succes ana bb3t value fe al ctor
        //3ndy 2 ctor 1 ab3tlo value fe 7alet al Ok , 2 hb3tlo error w s3tha la value b defualt 
        public Result(TValue value) : base(true, Array.Empty<Error>())
        {
            _value = value;
        }

        //bb3t 4akl tany ll Fail
        //-2
        private Result(Error error) : base(false, new[] { error })
        {
            //defualt 34an ana msh 3arf hwa ba3t eh
            _value = default!;
        }
        //4akl 3 bya5od collection mn al errors 
        private Result(IReadOnlyList<Error> errors) : base(false, errors)
        {
            //defualt 34an ana msh 3arf hwa ba3t eh
            _value = default!;
        } 
        #endregion

        //hna b2olo low sucess ana 3ayz al value y 7mada XD aw al data
        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);

        // hna ana m7tag ctor gded 34an 4akl gded
        //-2
        //hna bb3t al error bs l2ny aslun ana 3aml chain 3al al issuccees b false
        public static Result<TValue> Fail(Error error) => new Result<TValue>(error);
        public static Result<TValue> Fail(IReadOnlyList<Error>error) => new Result<TValue>(error);





        //Operator OverLoading
         // aftkr As2l 3ala de 34an low mt2ate4

        //public static implicit operator Result<TValue>(TValue value) => Ok(value);

        //public static implicit operator Result<TValue>(Error error) => Fail(error);






    }


}
