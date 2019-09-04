using System;
using DeepClone.Model;
using System.Collections.Generic;
using Natasha;

namespace DeepClone.Template
{
    public class CloneTemplate : ICloneTemplate
    {


        private readonly HashSet<ICloneTemplate> _handlers;
        public CloneTemplate()
        {
            _handlers = new HashSet<ICloneTemplate>();
        }




        public bool MatchType(Type type)
        {
            throw new NotImplementedException();
        }




        /// <summary>
        /// 注册处理类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public CloneTemplate Register<T>() where T: ICloneTemplate,new()
        {
            _handlers.Add(new T());
            return this;
        }





        public Delegate TypeRouter(NBuildInfo info)
        {

            foreach (var item in _handlers)
            {
                if (item.MatchType(info.DeclaringType))
                {
                    return item.TypeRouter(info.DeclaringType);
                }
            }
            return default;

        }
    }
}
