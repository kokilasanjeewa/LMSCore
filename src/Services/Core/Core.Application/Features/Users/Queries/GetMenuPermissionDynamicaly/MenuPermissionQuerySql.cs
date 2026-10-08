namespace Core.Application.Features.Users.Queries.GetMenuPermissionDynamicaly
{
    internal static class MenuPermissionQuerySql
    {
        private const string MenuColumns = @"
            SELECT
                ROW_NUMBER() OVER (ORDER BY m.MnuPosition) AS Id,
                m.MnuID,
                m.ModSerialID,
                module.ModName,
                m.MnuLevel,
                m.IsShown,
                ISNULL(m.ParentID, 0) AS ParentID,
                m.Active,
                m.IsDeleted,
                m.MnuName,
                m.MnuPosition,
                m.MnuText,
                m.PageName,
                m.MnuMTBR";

        public const string AllMenus = MenuColumns + @",
                CAST(0 AS BIT) AS IsChecked
            FROM [Core].[Menu] AS m
            LEFT JOIN [Core].[Module] AS module ON m.ModSerialID = module.ModSerialID
            WHERE m.IsShown = 1
              AND m.IsDeleted = 0
              AND m.MnuMTBR IN ('M', 'T')
            ORDER BY m.MnuPosition";

        public const string UserMenus = MenuColumns + @",
                CAST(CASE WHEN um.UserMnuPermsSerialID IS NOT NULL THEN 1 ELSE 0 END AS BIT) AS IsChecked
            FROM [Core].[Menu] AS m
            LEFT JOIN [Core].[Module] AS module ON m.ModSerialID = module.ModSerialID
            LEFT JOIN [Core].[UserMenuPermission] AS um
                ON um.MnuID = m.MnuID
                AND um.UserSerialID = @userSerialID
                AND um.IsDeleted = 0
                AND um.Active = 1
            WHERE m.IsShown = 1
              AND m.IsDeleted = 0
              AND m.MnuMTBR IN ('M', 'T')
            ORDER BY m.MnuPosition";

        public const string GroupMenus = MenuColumns + @",
                CAST(CASE WHEN gm.GrpMnuSerialID IS NOT NULL THEN 1 ELSE 0 END AS BIT) AS IsChecked
            FROM [Core].[Menu] AS m
            LEFT JOIN [Core].[Module] AS module ON m.ModSerialID = module.ModSerialID
            LEFT JOIN [Core].[GroupMenu] AS gm
                ON gm.MnuID = m.MnuID
                AND gm.GrpSerialID = @grpSerialID
                AND gm.IsDeleted = 0
                AND gm.Active = 1
            WHERE m.IsShown = 1
              AND m.IsDeleted = 0
              AND m.MnuMTBR IN ('M', 'T')
            ORDER BY m.MnuPosition";
    }
}
