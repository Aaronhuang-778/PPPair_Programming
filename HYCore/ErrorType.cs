using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYCore
{
    public class ErrorType
    {
        public enum code
        {
            not_known
        }
    }

    public class InputErrorType : ErrorType
    {
        public new enum code
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
            no_filename,
            no_input_text,
            no_check_mode,
            empty_string,
            unallowed_circle
        }
    }



    [Serializable]
    public class InvalidInputException : Exception
    {
        override public string Message { get; }

        public InvalidInputException() : base() { }


        public InvalidInputException(string message)
            : base(message)
        {
            Message = message;
            Console.WriteLine(message);
            Console.WriteLine($"[InvalidInputException message]: {message}");
        }

        

        public InvalidInputException(InputErrorType.code code)
            : base()
        {
            Console.WriteLine($"[InvalidInputException code]: {code}");
            string message = "";
            switch (code)
            {
                case InputErrorType.code.dupli_para:
                    message = "The parameter has already appeared or you have chosen output type!";
                    break;
                case InputErrorType.code.not_support:
                    message = "This command is not supported!";
                    break;
                case InputErrorType.code.wrong_format:
                    message = "Please check your command's format!";
                    break;
                case InputErrorType.code.wrong_head:
                    message = "Please check your -h's character!";
                    break;
                case InputErrorType.code.wrong_tail:
                    message = "Please check your -t's character!";
                    break;
                case InputErrorType.code.illegal_path:
                    message = "Your file path is illegal!";
                    break;
                case InputErrorType.code.illegal_file_type:
                    message = "Your file type is not .txt!";
                    break;
                case InputErrorType.code.file_not_found:
                    message = "Can't find your file!";
                    break;
                case InputErrorType.code.illegal_para_combination:
                    message = "Please check your parameters combination, " +
                        "we don't support your combination!";
                    break;
                case InputErrorType.code.no_filename:
                    message = "Please input your words file!";
                    break;
                case InputErrorType.code.no_input_text:
                    message = "Please input your words in the text box!";
                    break;
                case InputErrorType.code.no_check_mode:
                    message = "Please check your calculating mode!";
                    break;
                case InputErrorType.code.unallowed_circle:
                    message = "Your words-file has illegal circle!";
                    break;
                case InputErrorType.code.empty_string:
                    message = "Your input words string to be calculated is empty!";
                    break;
                default:
                    message = "This command is not supported!";
                    break;
            }
            //Environment.Exit(0);
            Message = message;
            Console.WriteLine(message);
        }
    }


    [Serializable]
    public class CircleException : Exception
    {

        public CircleException()
            : base("Words list has circle without -r!")
        {
            Console.WriteLine("CircleException: Words list has circle without -r!");
            //Environment.Exit(0);
        }
    }

    public class ChainErrorType : ErrorType
    {
        public new enum code
        {
            head_not_found,
            tail_not_found,
            chain_not_found
        }
    }

    
    [Serializable]
    public class ChainNotFoundException : Exception
    {
        override public string Message { get; }

        public ChainNotFoundException()
            : base($"ChainNotFoundException")
        {
            Message = "The word chain that you asked for is not found!";
            Console.WriteLine($"The word chain that you asked for is not found!");
            //Environment.Exit(0);
        }

        public ChainNotFoundException(ChainErrorType.code code)
                : base()
        {
            Console.WriteLine($"[ChainNotFoundException code]: {code}");
            String message = "";
            switch (code)
            {
                case ChainErrorType.code.head_not_found:
                    message = "There's no words start with the head you asked for!";
                    break;
                case ChainErrorType.code.tail_not_found:
                    message = "There's no words end with the tail you asked for!";
                    break;
                case ChainErrorType.code.chain_not_found:
                    message = "The word chain that you asked for is not found!";
                    break;
                default:
                    message = "The word chain that you asked for is not found!";
                    break;
            }
            //Environment.Exit(0);
            Message = message;
            Console.WriteLine(message);
        }
    }
}
