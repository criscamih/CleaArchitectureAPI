using System;
using SocialMediaCore.CustomEntities;

namespace SocialMediaApi.Responses
{
   public class Response<T>
   {
      public Response(T data)
      {
          Data = data;
      }

      public T Data { get; set; }

      public PageMetadata meta { get; set; }
   }
}