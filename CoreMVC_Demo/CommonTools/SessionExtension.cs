using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Demo.CommonTools
{
    //Session'i Extension haline getirmemizin nedeni kompleks tiplerimizi alması gerektigindendir. Extension metotlar sadece generic olmayan static class'larda tanımlanabilir. (Yani aşağıdaki metotlar bir isimlendirme değil Extension metotlardır; SetObject, GetObject)

    //Static olması gerekir
    public static class SessionExtension
    {
        //Session'imizi belirleyecek metodu yaratıyoruz

        //
        public static void SetObject(this ISession session,string key, object value)
        {
            //serialize etmek Json Stringe çevirmek anlamına gelir
            string objectString = JsonConvert.SerializeObject(value);
            session.SetString(key, objectString);
        }


        //Sessionı attık geri almak lazım. Generic metotlar ile

        //T : class vermemizin nedeni T'ye class dışında bir property verilmesin istiyoruz
        public static T GetObject<T>(this ISession session, string key) where T : class //(T bir referans tip olmak zorundadır)
        {
            string objectString = session.GetString(key); //GetString ile key'i aldık
            if (string.IsNullOrEmpty(objectString))
            {
                return null;
            }
            T deserializedObject = JsonConvert.DeserializeObject<T>(objectString);
            return deserializedObject; //
        }


    }
}
