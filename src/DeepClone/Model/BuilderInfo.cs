using Natasha;
using System;
using System.Reflection;

namespace DeepClone.Model
{
    /// <summary>
    /// 构建信息
    /// </summary>
    public class BuilderInfo
    {
        public Type DeclaringType;
        public string DeclaringTypeName;
        public string DeclaringAvailableName;

        public Type MemberType;
        public string MemberTypeName;
        public string MemberTypeAvailableName;
        public string MemberName;

        public Type ElementType;
        public string ElementTypeName;
        public string ElementTypeAvailableName;
        public int ArrayLayer;
        public int ArrayDimensions;
        public bool IsStatic;


        public string StaticName;

        public static implicit operator BuilderInfo(MemberInfo info)
        {
            if (info.MemberType == MemberTypes.Field)
            {

                var tempInfo = (FieldInfo)(info);

                var instance = new BuilderInfo
                {
                    DeclaringType = tempInfo.DeclaringType,
                    DeclaringTypeName = tempInfo.DeclaringType.GetDevelopName(),
                    DeclaringAvailableName = tempInfo.DeclaringType.GetAvailableName(),


                    MemberName = tempInfo.Name,
                    MemberType = tempInfo.FieldType,
                    MemberTypeName = tempInfo.FieldType.GetDevelopName(),
                    MemberTypeAvailableName = tempInfo.FieldType.GetAvailableName(),

                };


                if (tempInfo.FieldType.IsArray)
                {

                    Type temp = tempInfo.FieldType;
                    int count = 0;
                    while (temp.HasElementType)
                    {

                        count++;
                        temp = temp.GetElementType();
                        
                    }
                    instance.ElementType = temp;
                    instance.ArrayLayer = count;


                    var ctor = tempInfo.FieldType.GetConstructors()[0];
                    instance.ArrayDimensions = ctor.GetParameters().Length;


                    instance.ElementTypeName = instance.ElementType.GetDevelopName();
                    instance.ElementTypeAvailableName = instance.ElementType.GetAvailableName();

                }


                

                instance.IsStatic = tempInfo.IsStatic;
                if (instance.IsStatic)
                {
                    instance.StaticName = $"{instance.DeclaringTypeName}";
                }
                return instance;

            }
            else if (info.MemberType == MemberTypes.Property)
            {

                var tempInfo = (PropertyInfo)(info);

                var instance = new BuilderInfo
                {

                    DeclaringType = tempInfo.DeclaringType,
                    DeclaringTypeName = tempInfo.DeclaringType.GetDevelopName(),
                    DeclaringAvailableName = tempInfo.DeclaringType.GetAvailableName(),


                    MemberName = tempInfo.Name,
                    MemberType = tempInfo.PropertyType,
                    MemberTypeName = tempInfo.PropertyType.GetDevelopName(),
                    MemberTypeAvailableName = tempInfo.PropertyType.GetAvailableName(),

                };

                if (tempInfo.PropertyType.IsArray)
                {

                    Type temp = tempInfo.PropertyType;
                    int count = 0;
                    while (temp.HasElementType)
                    {

                        count++;
                        temp = temp.GetElementType();

                    }
                    instance.ElementType = temp;
                    instance.ArrayLayer = count;


                    var ctor = tempInfo.PropertyType.GetConstructors()[0];
                    instance.ArrayDimensions = ctor.GetParameters().Length;


                    instance.ElementTypeName = instance.ElementType.GetDevelopName();
                    instance.ElementTypeAvailableName = instance.ElementType.GetAvailableName();

                }
                

                instance.IsStatic = tempInfo.GetGetMethod(true).IsStatic;
                if (instance.IsStatic)
                {
                    instance.StaticName = $"{instance.DeclaringTypeName}";
                }
                return instance;

            }

            return null;
        }

        public static implicit operator BuilderInfo(Type type)
        {
            var instance = new BuilderInfo
            {
                DeclaringType = type,
                DeclaringTypeName = type.GetDevelopName(),
                DeclaringAvailableName = type.GetAvailableName(),
            };

            if (type.IsArray)
            {

                Type temp = type;
                int count = 0;
                while (temp.HasElementType)
                {

                    count++;
                    temp = temp.GetElementType();

                }
                instance.ElementType = temp;
                instance.ArrayLayer = count;


                var ctor = type.GetConstructors()[0];
                instance.ArrayDimensions = ctor.GetParameters().Length;


                instance.ElementTypeName = instance.ElementType.GetDevelopName();
                instance.ElementTypeAvailableName = instance.ElementType.GetAvailableName();

            }
            
            return instance;
        }
    }
}
