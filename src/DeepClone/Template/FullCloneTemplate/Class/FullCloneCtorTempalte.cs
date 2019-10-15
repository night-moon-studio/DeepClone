using Natasha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DeepClone.Template
{
    public class FullCloneCtorTempalte
    {


        private HashSet<Dictionary<string, Type>> _ctors;
        private Dictionary<Dictionary<string, Type>, Dictionary<string, string>> _parametersMapping;
        private Dictionary<Dictionary<string, Type>, int> _values;


        public FullCloneCtorTempalte(Type type)
        {

            _ctors = new HashSet<Dictionary<string, Type>>();
            _values = new Dictionary<Dictionary<string, Type>, int>();
            _parametersMapping = new Dictionary<Dictionary<string, Type>, Dictionary<string, string>>();
            var temp = type.GetConstructors();


            //获取所有构造函数
            for (int i = 0; i < temp.Length; i++)
            {

                var dict = new Dictionary<string, Type>();
                _ctors.Add(dict);
              

                //匹配频次字典
                _values[dict] = 0;


                //初始化构造字典
                _parametersMapping[dict] = new Dictionary<string, string>();
                var parameters = temp[i].GetParameters();
                foreach (var item in parameters)
                {

                    //缓存构造信息
                    dict[item.Name.ToUpper()] = item.ParameterType;
                    _parametersMapping[dict][item.Name.ToUpper()] = item.Name;

                }

            }

        }




        public string GetCtor(IEnumerable<NBuildInfo> infos)
        {

            Dictionary<string, Type> pairs = default;
            int preResult = 0;
            foreach (var item in _ctors)
            {

                int value = 0;

                //遍历符合条件的成员
                foreach (var info in infos)
                {

                    //从参数缓存中查找是否存在该类型
                    var name = info.MemberTypeAvailableName;
                    if (item.ContainsKey(name))
                    {

                        if (item[name] == info.MemberType)
                        {
                            //频次+1
                            value += 1;
                        }

                    }

                }


                if (preResult < value)
                {
                    //选择最大的匹配节点
                    preResult = value;
                    pairs = item;
                }


            }

            if (pairs != default)
            {

                //通过最高频次的匹配字典找到参数真名缓存
                var cache = _parametersMapping[pairs];


                //生成脚本
                StringBuilder scriptBuilder = new StringBuilder();
                foreach (var item in infos)
                {

                    var name = item.MemberName;
                    if (cache.ContainsKey(name))
                    {

                        //如果名字和类型都匹配上了
                        if (pairs[name] == item.MemberType)
                        {
                            scriptBuilder.Append($"{cache[name]}:old.{item.MemberName},");
                            pairs.Remove(name);
                        }
                        
                    }
                   

                }


                //没匹配上的都传default
                foreach (var item in pairs)
                {
                    scriptBuilder.Append($"{cache[item.Key]}:default,");
                }


                if (scriptBuilder.Length>0)
                {
                    scriptBuilder.Length -= 1;
                }
                return scriptBuilder.ToString();

            }

            return default;

        }

    }

}
