using System;

namespace DotNetDemos.CSharpExamples
{
    public class Tricky
    {
        public void Play()
        {
            //var result = ReturnTryCatchFinally();
        }

        public string ReturnTryCatchFinally()
        {
            try
            {
                return "a";
            }
            catch (Exception)
            {
                return "b";
            }
            finally
            {
                //Compile time error: controll can not leave the body of Finally clause
                //return "c";
            }
        }
        
    }  
}
