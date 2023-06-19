using Clean.Frame.Apps.TargetBoard.Models;

namespace Clean.Frame.Apps.TargetBoard.AutoMapper
{
    public class AutoMapperConfig
    {
        public static Type[] RegisterMappings()
        {
            return new Type[]
            {
                typeof(AutoMapperProfile),
            };
        }
    }
}
