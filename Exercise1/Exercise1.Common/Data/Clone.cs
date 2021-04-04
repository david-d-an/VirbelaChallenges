using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Exercise1.Common.Data {
  public static class DataCloneExtension {
    public static T DeepClone<T>(this T obj) {
      if (obj.GetType().IsSerializable) {
        using (var ms = new MemoryStream()) {
          var formatter = new BinaryFormatter();
          formatter.Serialize(ms, obj);
          ms.Position = 0;
          return (T)formatter.Deserialize(ms);
        }
      }
      return default(T);
    }

    public static object CloneObject(this object objSource) {
      //Get the type of source object and create a new instance of that type
      Type typeSource = objSource.GetType();
      object objTarget = Activator.CreateInstance(typeSource);

      //Get all the properties of source object type
      PropertyInfo[] propertyInfo = typeSource.GetProperties(
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

      //Assign all source property to taget object 's properties
      foreach (PropertyInfo property in propertyInfo) {
        //Check whether property can be written to
        if (property.CanWrite) {
          //check whether property type is value type, enum or string type
          if (property.PropertyType.IsValueType
          || property.PropertyType.IsEnum
          || property.PropertyType.Equals(typeof(System.String))) {
            property
              .SetValue(objTarget, property
              .GetValue(objSource, null), null);
          }
          //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
          else {
            object objPropertyValue = property.GetValue(objSource, null);

            if (objPropertyValue == null) {
              property.SetValue(objTarget, null, null);
            }
            else {
              property.SetValue(objTarget, objPropertyValue.CloneObject(), null);
            }
          }
        }
      }

      return objTarget;
    }

    public static void CopyProperties(object objSource, object objDestination) {
      //get the list of all properties in the destination object
      var destProps = objDestination.GetType().GetProperties();

      //get the list of all properties in the source object
      foreach (var sourceProp in objSource.GetType().GetProperties()) {
        foreach (var destProp in destProps) {
          //if we find match between source & destination properties name, set
          //the value to the destination property
          if (destProp.Name == sourceProp.Name 
          && destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType)) {
              var sourcePropVal = sourceProp.GetValue(sourceProp, new object[] { });
              destProp
                .SetValue(destProps, sourcePropVal, new object[] { });
              break;
          }
        }
      }
    }
  }
}
