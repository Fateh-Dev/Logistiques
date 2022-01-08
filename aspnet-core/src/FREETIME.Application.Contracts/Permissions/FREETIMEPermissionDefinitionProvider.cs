using FREETIME.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace FREETIME.Permissions
{
    public class FREETIMEPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(FREETIMEPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(FREETIMEPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FREETIMEResource>(name);
        }
    }
}
