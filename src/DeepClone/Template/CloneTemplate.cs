using DeepClone.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
        /// 注册类处理操作类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public CloneTemplate Register<T>() where T: ICloneTemplate,new()
        {
            _handlers.Add(new T());
            return this;
        }





        public Delegate TypeRouter(Type type)
        {
            return default;
        }
    }
}
