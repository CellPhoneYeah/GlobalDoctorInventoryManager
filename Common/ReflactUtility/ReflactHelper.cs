using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommonUtility
{
    public class ReflactHelper
    {
        /// <summary>
        /// Assign values to objects by reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetValue"></param>
        /// <param name="targetProperty"></param>
        /// <param name="targetEntity"></param>
        public static void SetValueToObject<T>(object targetValue
            , PropertyInfo targetProperty
            , T targetEntity)
        {
            try
            {
                Type entityType = typeof(T);
                PropertyInfo[] allProperties = entityType.GetProperties();
                if (allProperties.ToList().Exists(x => x.Name == targetProperty.Name))
                {
                    targetProperty.SetValue(targetEntity, targetValue, null);
                }
                else
                {
                    throw new Exception("准备设置值的对象不存在名为" + targetProperty.Name + "的属性");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
