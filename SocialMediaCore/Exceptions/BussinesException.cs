using System;

namespace SocialMediaCore.Exceptions
{
   public class BussinesException : Exception
   {
      public BussinesException()
      {
          
      }

      public BussinesException(string message) : base(message)
      {
          
      }
   }
}