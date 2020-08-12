using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TTT
{
    public static class GenericExtensions
    {
        public static OutputT GenericTypeCast<InputT, OutputT>(this InputT Input)
        {
            return JsonConvert.DeserializeObject<OutputT>(JsonConvert.SerializeObject(Input));
        }

        /// <summary>
        /// Takes a model to merge with the original.  Non-null properties are overwritten, while null ones are not modified.
        /// </summary>
        /// <typeparam name="T">Type of the models being merged</typeparam>
        /// <param name="OriginalObject">The original object, that will also become the merged model that is returned</param>
        /// <param name="UpdatedObject">The model containing modified properties to be merged with the original</param>
        /// <returns>The merged model</returns>
        public static T Merge<T>(this T OriginalObject, T UpdatedObject)
        {
            Type ObjectType = typeof(T);
            IEnumerable<PropertyInfo> Properties = ObjectType.GetProperties(BindingFlags.Public).Where(p => p.CanRead && p.CanWrite);

            foreach(PropertyInfo property in Properties)
            {
                var updatedValue = property.GetValue(UpdatedObject);
                if (updatedValue != null)
                {
                    property.SetValue(OriginalObject, updatedValue);
                }
            }

            return OriginalObject;
        }
    }
}
