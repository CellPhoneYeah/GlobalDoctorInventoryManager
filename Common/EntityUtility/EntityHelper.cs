using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CommonUtility
{
    public class EntityHelper
    {
        /// <summary>
        /// A shallow copy of an object
        /// </summary>
        /// <typeparam name="EntityType"></typeparam>
        /// <param name="originEntity"></param>
        /// <returns></returns>
        public static EntityType ShallowCopy<EntityType>(EntityType originEntity,EntityType targetEntity)where EntityType:new ()
        {
            Type entityType = typeof(EntityType);
            PropertyInfo[] properties = entityType.GetProperties();
            object tempPropertyValue = null;
            foreach (PropertyInfo curProperty in properties)
            {
                tempPropertyValue = null;
                tempPropertyValue = curProperty.GetValue(targetEntity, null);
                ReflactHelper.SetValueToObject<EntityType>(tempPropertyValue, curProperty, originEntity);
            }
            return originEntity;
        }
    }
}
