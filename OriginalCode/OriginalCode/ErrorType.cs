using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCode
{
    class ErrorType
    {
        public enum code
        {
            not_known
        }
    }

    class InputErrorType : ErrorType
    {
        public enum code
        {
            dupli_para,
            not_support, //default
            wrong_format,
            wrong_head,
            wrong_tail,
            illegal_path, 
            illegal_file_type, 
            file_not_found,
            illegal_para_combination,
            no_filename
        }
    }



    [Serializable]
    class InvalidInputException : Exception
    {
        public InvalidInputException() { }

        public InvalidInputException(InputErrorType.code code)
            : base($"Invalid Input code: {code}")
        {
            Console.WriteLine($"[InvalidInputException code]: {code}");
            switch (code)
            {
                case InputErrorType.code.dupli_para:
                    Console.WriteLine("The parameter has already appeared or you have chosen output type!");
                    break;
                case InputErrorType.code.not_support:
                    Console.WriteLine("This command is not supported!");
                    break;
                case InputErrorType.code.wrong_format:
                    Console.WriteLine("Please check your command's format!");
                    break;
                case InputErrorType.code.wrong_head:
                    Console.WriteLine("Please check your -h's character!");
                    break;
                case InputErrorType.code.wrong_tail:
                    Console.WriteLine("Please check your -t's character!");
                    break;
                case InputErrorType.code.illegal_path:
                    Console.WriteLine("Your file path is illegal!");
                    break;
                case InputErrorType.code.illegal_file_type:
                    Console.WriteLine("Your file type is not .txt!");
                    break;
                case InputErrorType.code.file_not_found:
                    Console.WriteLine("Can't find your file!");
                    break;
                case InputErrorType.code.illegal_para_combination:
                    Console.WriteLine("Please check your parameters combination, " +
                        "we don't support your combination!");
                    break;
                case InputErrorType.code.no_filename:
                    Console.WriteLine("Please input your words file!");
                    break;
                default:
                    Console.WriteLine("This command is not supported!");
                    break;
            }
            Environment.Exit(0);
        }
    }
}
