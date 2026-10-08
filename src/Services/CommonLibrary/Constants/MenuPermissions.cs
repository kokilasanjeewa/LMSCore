

using System;
using System.Reflection;

namespace JwtTokenAuthentication.Constants
{
    public static partial class Permissions
    {
        // ========================== Inventory Module ==========================
        public const int Mnu_Inventory = 2493; // parent0 // mnu level0 // ModuleInventory

        // ----- Inventory Menus -----
        public const int Mnu_GoodsReceive = 2494;     // parent2493 // mnu level1
        public const int Mnu_PurchaseReturn = 2495;   // parent2493 // mnu level1
        public const int Mnu_Dispatch = 2496;         // parent2493 // mnu level1
        public const int Mnu_RentalReturn = 2501;     // parent2493 // mnu level1
        public const int Mnu_SalesReturn = 2502;      // parent2493 // mnu level1
        public const int Mnu_GoodsIssue = 2503;       // parent2493 // mnu level1
        public const int Mnu_IssueReturn = 2504;      // parent2493 // mnu level1
        public const int Mnu_Stock = 3201;
        public const int Mnu_StockTransferIn = 2505;  // parent2493 // mnu level1
        public const int Mnu_StockTransferOut = 2506; // parent2493 // mnu level1
        public const int Mnu_StockAdjustment = 2507;  // parent2493 // mnu level1
        public const int Mnu_StockMovement = 2508;    // parent2493 // mnu level1

        // ----- Dispatch Submenu -----
        public const int Mnu_DispatchRentals = 2497;  // parent2496 // mnu level2
        public const int Mnu_DispatchSales = 2498;    // parent2496 // mnu level2
        public const int Mnu_DispatchGeneral = 2499;  // parent2496 // mnu level2
        public const int Mnu_DispatchTransfer = 2500; // parent2496 // mnu level2

        // ----- Goods Receive (GRN) -----
        public const int Tab_GRN = 2509;             // parent2494 // mnu level2
        public const int Tab_ForApproval = 2510;     // parent2494 // mnu level2
        public const int Tab_GRNLines = 2511;        // parent2494 // mnu level2
        public const int Tab_GRNDetails = 2512;      // parent2494 // mnu level2
        public const int Tab_GRNHeader = 2513;       // parent2494 // mnu level2

        // GRN Buttons
        public const int Btn_GRNAdd = 2514;          // parent2494
        public const int Btn_GRNReport = 2515;       // parent2494
        public const int Btn_GRNReset = 2516;        // parent2494
        public const int Btn_GRNHelp = 2517;         // parent2494
        public const int Btn_GRNClose = 2518;        // parent2494
        public const int Btn_GRNView = 2519;         // parent2494
        public const int Btn_GRNApprove = 2520;      // parent2494
        public const int Btn_GRNDelete = 2521;       // parent2494
        public const int Btn_GRNEdit = 2522;         // parent2494
        public const int Btn_GRNLinesEdit = 2523;    // parent2494
        public const int Btn_GRNPrint = 2524;        // parent2494
        public const int Btn_GRNSave = 2667;         // parent2494

        // ----- Purchase Return (PRN) -----
        public const int Tab_PRN = 2557;             // parent2495
        public const int Tab_PRNForApproval = 2558;  // parent2495
        public const int Tab_PRNHeader = 2559;       // parent2495
        public const int Tab_PRNLines = 2560;        // parent2495
        public const int Tab_PRNDetails = 2561;      // parent2495

        // PRN Buttons
        public const int Btn_PRNAdd = 2562;          // parent2495
        public const int Btn_PRNReport = 2563;       // parent2495
        public const int Btn_PRNReset = 2564;        // parent2495
        public const int Btn_PRNHelp = 2565;         // parent2495
        public const int Btn_PRNClose = 2566;        // parent2495
        public const int Btn_PRNView = 2567;         // parent2495
        public const int Btn_PRNApprove = 2568;      // parent2495
        public const int Btn_PRNDelete = 2569;       // parent2495
        public const int Btn_PRNEdit = 2570;         // parent2495
        public const int Btn_PRNLinesEdit = 2571;    // parent2495
        public const int Btn_PRNLinesDelete = 2572;  // parent2495
        public const int Btn_PRNPrint = 2573;        // parent2495
        public const int Btn_PRNSave = 2574;         // parent2495


        // ========================== Rental Module ==========================
        public const int Mnu_Rental = 1121; // parent0

        // ----- Rental Menus -----
        public const int Mnu_Customer = 2736;          // parent1121
        public const int Mnu_CustomerLocation = 2741;  // parent1121

        // Customer Buttons
        public const int Btn_CustomerSave = 2737;      // parent2736
        public const int Btn_CustomerDelete = 2738;    // parent2736
        public const int Btn_CustomerEdit = 2739;      // parent2736
        public const int Btn_CustomerView = 2740;      // parent2736

        // Customer Location Buttons
        public const int Btn_CustomerLocationSave = 2742;   // parent2741
        public const int Btn_CustomerLocationEdit = 2743;   // parent2741
        public const int Btn_CustomerLocationView = 2744;   // parent2741
        public const int Btn_CustomerLocationDelete = 2745; // parent2741


        // ========================== Sales Module ==========================
        public const int Mnu_Sales = 1122; // parent0


        // ========================== Purchasing Module ==========================
        public const int Mnu_Purchasing = 1123; // parent0

        // ----- Purchasing Menus -----
        public const int Mnu_Supplier = 2721;        // parent1123
        public const int Mnu_SupplierInvoice = 2728; // parent1123

        // Supplier Buttons
        public const int Btn_CloseSupplier = 2722;
        public const int Btn_ViewSupplier = 2723;
        public const int Btn_SaveSupplier = 2724;
        public const int Btn_ApproveSupplier = 2725;
        public const int Btn_DeleteSupplier = 2726;
        public const int Btn_EditSupplier = 2727;

        public const int Btn_SupplierAdd = 3416;
        public const int Btn_SupplierReport = 3417;
        public const int Btn_SupplierReset = 3418;
        public const int Tab_SupplierForApprove = 3410;
        public const int Tab_Supplier = 3411;
        public const int Tab_SupplierGeneralData = 3412;
        public const int Tab_SupplierContactDetail = 3413;
        public const int Tab_SupplierBusinessInfo = 3414;
        public const int Tab_SupplierPaymentDetail = 3415;

        // Supplier Invoice Buttons
        public const int Btn_CloseSupInvoice = 2729;
        public const int Btn_ViewSupInvoice = 2730;
        public const int Btn_SaveSupInvoice = 2731;
        public const int Btn_ApproveSupInvoice = 2732;
        public const int Btn_DeleteSupInvoice = 2733;
        public const int Btn_EditSupInvoice = 2734;
        public const int Btn_EditSupInvoiceLines = 2735;


        // ========================== Operation Module ==========================
        public const int Mnu_Operation = 1052; // parent0


        // ========================== Asset Management Module ==========================
        public const int Mnu_AsstMgt = 4; // parent0

        // ----- Asset Management Menus -----
        public const int Mnu_AsstReg = 5;                 // parent4
        public const int Mnu_AstMovRnt = 2137;            // parent4
        public const int Mnu_AstFinDtal = 2138;           // parent4
        public const int Mnu_AstDetAsset = 2140;          // parent4
        public const int Mnu_AssetOwnershipTransfer = 2639;// parent4

        // ----- Asset Movement -----
        public const int Mnu_AssetMovementRental = 2342;  // parent2137
        public const int Mnu_AssetMovementInternal = 2343;// parent2137

        // ----- Asset Financial Details -----
        public const int Tab_AssetFinancialDetailsTabAsset = 2361;
        public const int Tab_AssetFinancialDetailsForApproval = 2362;
        public const int Tab_AssetFinancialDetailsAssetDetail = 2363;
        public const int Tab_AssetFinancialDetailsfinancial1 = 2364;
        public const int Tab_AssetFinancialDetailsfinancial2 = 2365;

        // Buttons - Asset Financial Details
        public const int Btn_AssetFinancialDetailsSave = 2366;
        public const int Btn_AssetFinancialDetailsReport = 2367;
        public const int Btn_AssetFinancialDetailsReset = 2368;
        public const int Btn_AssetFinancialDetailsClose = 2369;
        public const int Btn_AssetFinancialDetailsHelp = 2370;
        public const int Btn_AssetFinancialDetailsAdd = 2371;
        public const int Btn_AssetFinancialDetailsApproval = 2412;
        public const int Btn_AssetFinancialDetailsDelete = 2413;
        public const int Btn_AssetFinancialDetailsEdit = 2414;
        public const int Btn_AssetFinancialDetailsView = 2669;

        // ----- Detach Assets -----
        public const int Tab_DetachAssetsTabAsset = 2385;
        public const int Tab_DetachAssetsTabForApproval = 2386;
        public const int Tab_DetachAssetsTabAssetDetail1 = 2387;
        public const int Tab_DetachAssetsTabAssetDetail2 = 2388;
        public const int Tab_DetachAssetsTabComponets = 2389;
        public const int Tab_DetachAssetsTabAttachment = 2390;

        // Buttons - Detach Assets
        public const int Btn_DetachAssetsSave = 2391;
        public const int Btn_DetachAssetsReport = 2392;
        public const int Btn_DetachAssetsReset = 2393;
        public const int Btn_DetachAssetsHelp = 2394;
        public const int Btn_DetachAssetsClose = 2395;
        public const int Btn_DetachAssetsAdd = 2396;
        public const int Btn_DetachAssetsEdit = 2397;
        public const int Btn_DetachAssetsDelete = 2398;
        public const int Btn_DetachAssetsApproval = 2525;
        public const int Btn_DetachAssetsView = 2668;


        // ---------------------- Asset Ownership ----------------------
        public const int Tab_AssetOwnershipTabAsset = 2372;
        public const int Tab_AssetOwnershipTabApproval = 2373;
        public const int Tab_AssetOwnershipTabAssetDetail = 2374;
        public const int Tab_AssetOwnershipTransfer = 2375;

        // Buttons - Asset Ownership
        public const int Btn_AssetOwnershipSave = 2376;
        public const int Btn_AssetOwnershipReport = 2377;
        public const int Btn_AssetOwnershipReset = 2378;
        public const int Btn_AssetOwnershipClose = 2379;
        public const int Btn_AssetOwnershipHelp = 2380;
        public const int Btn_AssetOwnershipEdit = 2381;
        public const int Btn_AssetOwnershipDelete = 2382;
        public const int Btn_AssetOwnershipAdd = 2384;
        public const int Btn_AssetOwnershipView = 2411;

        // ---------------------- Asset Ownership In ----------------------
        public const int Mnu_AstOwnTrnsIn = 2640;               // parent2639
        public const int Mnu_AstOwnTrns = 2653;                 // parent2639
        public const int Tab_AssetOwnershipInAsset = 2641;      // parent2640
        public const int Tab_AssetOwnershipInForApproval = 2642;// parent2640
        public const int Tab_AssetOwnershipInAssetDetail = 2643;// parent2640
        public const int Tab_AssetOwnershipTransferIn = 2644;   // parent2640

        // Buttons - Asset Ownership In
        public const int Btn_AssetOwnershipInSave = 2645;
        public const int Btn_AssetOwnershipInReport = 2646;
        public const int Btn_AssetOwnershipInReset = 2647;
        public const int Btn_AssetOwnershipInClose = 2648;
        public const int Btn_AssetOwnershipInHelp = 2649;
        public const int Btn_AssetOwnershipInEdit = 2650;
        public const int Btn_AssetOwnershipInDelete = 2651;
        public const int Btn_AssetOwnershipInApproval = 2652;

    
        // Asset Management
        public const int Btn_AssetOwnershipApproval = 2666; // parent2653 // mnu level3 // ModuleAsset Management

        // HRM and Payroll
        public const int Mnu_HRMandPayroll = 1059; // parent0 // mnu level0
        public const int Mnu_AttendanceManagement = 2605; // parent1059 // mnu level1
        public const int Mnu_Employees = 2615; // parent1059 // mnu level1
        public const int Mnu_LeaveManagement = 2620; // parent1059 // mnu level1
        public const int Mnu_OrganizationManagement = 2623; // parent1059 // mnu level1
        public const int Mnu_Payroll = 2627; // parent1059 // mnu level1

        // Attendance Submenus
        public const int Mnu_AttendanceManualUpload = 2606; // parent2605 // mnu level2
        public const int Mnu_AttendanceAdjustments = 2607; // parent2605 // mnu level2
        public const int Mnu_SetShift = 1062; // parent2605 // mnu level2
        public const int Mnu_FPDataUpload = 2767; // parent2605 // mnu level2

        // Employee Submenus
        public const int Mnu_EmployeePersonalDetails = 2616; // parent2615 // mnu level2
        public const int Mnu_WorkExperience = 2617; // parent2615 // mnu level2
        public const int Mnu_Qualifications = 2618; // parent2615 // mnu level2
        public const int Mnu_SpouseAndDependents = 2619; // parent2615 // mnu level2
        public const int Mnu_EmployeeOfficialDetails = 2806; // parent2615 // mnu level2
        public const int Mnu_CompanyProvidedAssets = 2818; // parent2615 // mnu level2

        // Leave Submenus
        public const int Mnu_LeaveEntry = 2621; // parent2620 // mnu level2
        public const int Mnu_LeaveAllocation = 2622; // parent2620 // mnu level2
        public const int Mnu_ShortLeaveEntry = 2870; // parent2620 // mnu level2
        public const int Mnu_LeaveApplication = 3072; // parent2620 // mnu level2

        // Payroll Submenus
        public const int Mnu_AllowanceEntry = 2901; // parent2627 // mnu level2
        public const int Mnu_EmolumentEntry = 2927; // parent2627 // mnu level2
        public const int Mnu_DeductionEntry = 2938; // parent2627 // mnu level2
        public const int Mnu_OTAdjustment = 3005; // parent2627 // mnu level2

        // HRM Buttons and Tabs
        public const int Btn_SetShiftDelete = 3109; // parent2608 // mnu level3
        public const int Btn_SetShiftAdd = 2609; // parent2608 // mnu level3
        public const int Btn_SetShiftReport = 2610; // parent2608 // mnu level3
        public const int Btn_SetShiftReset = 2611; // parent2608 // mnu level3
        public const int Btn_SetShiftView = 2612; // parent2608 // mnu level3
        public const int Btn_SetShiftEdit = 2613; // parent2608 // mnu level3
        public const int Btn_SetShiftSave = 2614; // parent2608 // mnu level3

        // Qualifications Tab
        public const int Tab_Qualifications = 2694; // parent2618 // mnu level3
        public const int Tab_QualificationsDetails = 2695; // parent2618 // mnu level3
        public const int Btn_AddQualifications = 2696; // parent2618 // mnu level3
        public const int Btn_ReportQualifications = 2697; // parent2618 // mnu level3
        public const int Btn_ResetQualifications = 2698; // parent2618 // mnu level3
        public const int Btn_ViewQualifications = 2699; // parent2618 // mnu level3
        public const int Btn_EditQualifications = 2700; // parent2618 // mnu level3
        public const int Btn_SaveQualifications = 2701; // parent2618 // mnu level3
        public const int Btn_AddQualificationDetails = 2702; // parent2618 // mnu level3
        public const int Btn_ViewQualificationDetails = 2703; // parent2618 // mnu level3
        public const int Btn_EditQualificationDetails = 2704; // parent2618 // mnu level3
        public const int Btn_HelpQualifications = 2705; // parent2618 // mnu level3
        public const int Btn_CloseQualifications = 2706; // parent2618 // mnu level3

        // Work Experience Tab
        public const int Tab_WorkExperience = 2707; // parent2617 // mnu level3
        public const int Tab_WorkExperienceDetails = 2708; // parent2617 // mnu level3
        public const int Btn_AddExperience = 2709; // parent2617 // mnu level3
        public const int Btn_ReportExperience = 2710; // parent2617 // mnu level3
        public const int Btn_ResetExperience = 2711; // parent2617 // mnu level3
        public const int Btn_ViewExperience = 2712; // parent2617 // mnu level3
        public const int Btn_EditExperience = 2713; // parent2617 // mnu level3
        public const int Btn_SaveExperience = 2714; // parent2617 // mnu level3
        public const int Btn_AddExperienceDetails = 2715; // parent2617 // mnu level3
        public const int Btn_ViewExperienceDetails = 2716; // parent2617 // mnu level3
        public const int Btn_EditExperienceDetails = 2717; // parent2617 // mnu level3
        public const int Btn_HelpExperience = 2718; // parent2617 // mnu level3
        public const int Btn_CloseExperience = 2719; // parent2617 // mnu level3

        // Attendance Buttons
        public const int Btn_AddAdjustAttendance = 2746; // parent2607 // mnu level3
        public const int Btn_ReportAdjustAttendance = 2747; // parent2607 // mnu level3
        public const int Btn_ResetAdjustAttendance = 2748; // parent2607 // mnu level3
        public const int Btn_EditAdjustAttendance = 2749; // parent2607 // mnu level3
        public const int Btn_SaveAdjustAttendance = 2750; // parent2607 // mnu level3
        public const int Btn_DeleteAdjustAttendance = 2751; // parent2607 // mnu level3
        public const int Btn_ViewAdjustAttendance = 2890; // parent2607 // mnu level3

        // Attendance Manual Upload Buttons
        public const int Btn_AddAttendanceManUpload = 2752; // parent2606 // mnu level3
        public const int Btn_ReportAttendanceManUpload = 2753; // parent2606 // mnu level3
        public const int Btn_ResetAttendanceManUpload = 2754; // parent2606 // mnu level3
        public const int Btn_EditAttendanceManUpload = 2755; // parent2606 // mnu level3
        public const int Btn_SaveAttendanceManUpload = 2756; // parent2606 // mnu level3
        public const int Btn_DeleteAttendanceManUpload = 2757; // parent2606 // mnu level3

        // Spouse and Dependents Buttons
        public const int Tab_SpouseAndDepntEmployees = 2758; // parent2619 // mnu level3
        public const int Tab_SpouseAndDependents = 2759; // parent2619 // mnu level3
        public const int Btn_AddSpouseAndDepnts = 2760; // parent2619 // mnu level3
        public const int Btn_ReportSpouseAndDepnts = 2761; // parent2619 // mnu level3
        public const int Btn_ResetSpouseAndDepnts = 2762; // parent2619 // mnu level3
        public const int Btn_EditSpouseAndDepnts = 2763; // parent2619 // mnu level3
        public const int Btn_SaveSpouseAndDepnts = 2764; // parent2619 // mnu level3
        public const int Btn_ViewSpouseAndDepnts = 2765; // parent2619 // mnu level3
        public const int Btn_DeleteSpouseAndDepnts = 2766; // parent2619 // mnu level3

        // FP Data Upload Buttons
        public const int Tab_ManualUpload = 2768; // parent2767 // mnu level3
        public const int Tab_UploadScheduleDetails = 2769; // parent2767 // mnu level3
        public const int Btn_AddFPDataUpload = 2770; // parent2767 // mnu level3
        public const int Btn_ReportFPDataUpload = 2771; // parent2767 // mnu level3
        public const int Btn_ResetFPDataUpload = 2772; // parent2767 // mnu level3
        public const int Btn_EditFPDataUpload = 2773; // parent2767 // mnu level3
        public const int Btn_DeleteFPDataUpload = 2774; // parent2767 // mnu level3
        public const int Btn_ViewFPDataUpload = 2775; // parent2767 // mnu level3
        public const int Btn_UploadFPDataUpload = 2776; // parent2767 // mnu level3
        public const int Btn_SaveFPDataUpload = 2777; // parent2767 // mnu level3
        public const int Btn_AddFPDataSchedule = 2778; // parent2767 // mnu level3
        public const int Btn_EditFPDataSchedule = 2779; // parent2767 // mnu level3
        public const int Btn_DeleteFPDataSchedule = 2780; // parent2767 // mnu level3

        // Personal Details Tabs & Buttons
        public const int Tab_PersonalDetails1 = 2793; // parent2616 // mnu level3
        public const int Tab_PersonalDetails2 = 2794; // parent2616 // mnu level3
        public const int Tab_PermanentResidence = 2795; // parent2616 // mnu level3
        public const int Tab_CurrentResidence = 2796; // parent2616 // mnu level3
        public const int Tab_ParentsDetails = 2797; // parent2616 // mnu level3
        public const int Tab_Nominee = 2798; // parent2616 // mnu level3
        public const int Btn_AddPersonalDetails = 2799; // parent2616 // mnu level3
        public const int Btn_ReportPersonalDetails = 2800; // parent2616 // mnu level3
        public const int Btn_ResetPersonalDetails = 2801; // parent2616 // mnu level3
        public const int Btn_SavePersonalDetails = 2802; // parent2616 // mnu level3
        public const int Btn_EditPersonalDetails = 2803; // parent2616 // mnu level3
        public const int Btn_ViewPersonalDetails = 2804; // parent2616 // mnu level3
        public const int Btn_DeletePersonalDetails = 2805; // parent2616 // mnu level3
        public const int Tab_PersonalDetails = 2807; // parent2806 // mnu level3
        public const int Tab_OfficialDetails = 2808; // parent2806 // mnu level3
        public const int Tab_SalaryAndEPF = 2809; // parent2806 // mnu level3
        public const int Btn_AddOfficialDetails = 2810; // parent2806 // mnu level3
        public const int Btn_ReportOfficialDetails = 2811; // parent2806 // mnu level3
        public const int Btn_ResetOfficialDetails = 2812; // parent2806 // mnu level3
        public const int Btn_SaveOfficialDetails = 2813; // parent2806 // mnu level3
        public const int Btn_EditOfficialDetails = 2814; // parent2806 // mnu level3
        public const int Btn_ViewOfficialDetails = 2815; // parent2806 // mnu level3
        public const int Btn_DeleteOfficialDetails = 2816; // parent2806 // mnu level3

        // Company Provided Assets Tabs & Buttons
        public const int Tab_Employees = 2819; // parent2818 // mnu level3
        public const int Tab_CompanyProvidedAssets = 2820; // parent2818 // mnu level3
        public const int Tab_AssetsDetails1 = 2821; // parent2818 // mnu level3
        public const int Tab_AssetsDetails2 = 2822; // parent2818 // mnu level3
        public const int Btn_AddCompanyAssets = 2823; // parent2818 // mnu level3
        public const int Btn_ReportCompanyAssets = 2824; // parent2818 // mnu level3
        public const int Btn_ResetCompanyAssets = 2825; // parent2818 // mnu level3
        public const int Btn_SaveCompanyAssets = 2826; // parent2818 // mnu level3
        public const int Btn_EditCompanyAssets = 2827; // parent2818 // mnu level3
        public const int Btn_ViewCompanyAssets = 2828; // parent2818 // mnu level3
        public const int Btn_DeleteCompanyAssets = 2829; // parent2818 // mnu level3

        // Leave Allocation Tabs & Buttons
        public const int Tab_LeaveAllocation = 2830; // parent2622 // mnu level3
        public const int Tab_LeaveBalance = 2831; // parent2622 // mnu level3
        public const int Btn_AddLeaveBalance = 2832; // parent2622 // mnu level3
        public const int Btn_ReportLeaveBalance = 2833; // parent2622 // mnu level3
        public const int Btn_ResetLeaveBalance = 2834; // parent2622 // mnu level3
        public const int Btn_SaveLeaveBalance = 2835; // parent2622 // mnu level3
        public const int Btn_EditLeaveBalance = 2836; // parent2622 // mnu level3
        public const int Btn_ViewLeaveBalance = 2837; // parent2622 // mnu level3
        public const int Btn_DeleteLeaveBalance = 2838; // parent2622 // mnu level3

        // =====================
        // Module: Setup / ModuleSetup
        // =====================

        // General Setup Menus
        public const int Mnu_SetDepartment = 1081;
        public const int Mnu_SetAdminSection = 1082;
        public const int Mnu_SetAdminBuilding = 1083;
        public const int Mnu_SetAdminFloor = 1084;
        public const int Mnu_SetProductionLine = 1085;
        public const int Mnu_SetAdminCountry = 1086;
        public const int Mnu_SetCompany = 1080;        // add 15/09/2025 change AdminCompany
        public const int Mnu_SetBankBranch = 1088;
        public const int Mnu_SetCurrency = 1089;
        public const int Mnu_SetBank = 1090;
        public const int Mnu_SetZone = 2129;
        public const int Mnu_SetRack = 2130;
        public const int Mnu_SetCustomerPriceCat = 2133;
        public const int Mnu_SetAttachment = 2134;
        public const int Mnu_SetCompatibleItem = 2135;
        public const int Mnu_SetBrandItemType = 2136;
        public const int Mnu_SetFpScanners = 2419;
        public const int Mnu_AssetItem = 2526;
        public const int Mnu_PollingDivision = 2575;
        public const int Mnu_SetJobTitle = 2587;
        public const int Mnu_SetDeletePopup = 2628;
        public const int Mnu_SetCalendar = 2677;
        public const int Mnu_SetCoveringDays = 3166;
        public const int Mnu_SetSpecialHoliday =3168;
        public const int Mnu_SetQualificationType = 2690;
        public const int Mnu_SetReason = 2845;
        public const int Mnu_SetDeduction = 2850;
        public const int Mnu_SetAllowances = 2854;
        public const int Mnu_SetSkillLevel = 2885;
        public const int Mnu_SetTransportRoute = 2892;
        public const int Mnu_SetEmolument = 2912;
        public const int Mnu_SetPayeTaxUpload = 2949;
        public const int Mnu_SetOpenPayrollPeriod = 2959;
        public const int Mnu_SetDeductionGroup = 3015;
        public const int Mnu_SetEmolumentGroup = 3016;
        public const int Mnu_SetDistrict = 3017;
        public const int Mnu_SetProvince = 3018;
        public const int Mnu_SetEthnicity = 3019;
        public const int Mnu_SetCompanyVehicles = 3021;
        public const int Mnu_SetLieuLeave = 3023; // 1037 Change to 
        public const int Mnu_SetDrivers = 3055;

        // =====================
        // Module: User Management
        // =====================
   //     public const int Mnu_UserManagement = 1038;

        // Tabs
        public const int Tab_SetUserDetails = 2293;
        public const int Tab_SetMenuPermissions = 2294;
        public const int Tab_SetButtonPermissions = 2304;
        public const int Tab_SetReportPermissions = 2305;
        public const int Tab_SetCompany = 2306;

        // Buttons
        public const int Btn_UserSave = 1119;
        public const int Btn_UserEdit = 2242;
        public const int Btn_UserReport = 2243;
        public const int Btn_UserReset = 2244;
        public const int Btn_UserAdd = 2245;
        public const int Btn_UserClone = 2246;
        public const int Btn_UserSearch = 2408;
        public const int Btn_UserDelete = 2409;
        public const int Btn_UserView = 2410;

        // Reports
        public const int Rpt_UserReport = 2308;

        // =====================
        // Module: Group Management
        // =====================
   //     public const int Mnu_GroupManagement = 1039;

        // Tabs
        public const int Tab_SetGroupDetails = 2311;
        public const int Tab_SetGroupMenu = 2312;

        // Buttons
        public const int Btn_GroupSave = 2309;
        public const int Btn_GroupReset = 2313;
        public const int Btn_GroupExport = 2314;
        public const int Btn_GroupEdit = 2315;
        public const int Btn_GroupDelete = 2316;
        public const int Btn_GroupView = 2317;

        // Reports
        public const int Rpt_GroupReport = 2310;

        // =====================
        // Module: Warehouse Management
        // =====================
    //    public const int Mnu_Warehouse = 1040;

        // Buttons
        public const int Btn_SetSaveWarehouse = 2143;
        public const int Btn_SetEditWarehouse = 2145;
        public const int Btn_SetHelpWarehouse = 2146;
        public const int Btn_SetResetWarehouse = 2147;
        public const int Btn_SetCloseWarehouse = 2148;
        public const int Btn_SetDeleteWarehouse = 2247;
        public const int Btn_SetViewWarehouse = 2248;

        // =====================
        // Module: Store Management
        // =====================
    //    public const int Mnu_Store = 1041;

        // Buttons
        public const int Btn_SetSaveStore = 2149;
        public const int Btn_SetEditStore = 2150;
        public const int Btn_SetHelpStore = 2151;
        public const int Btn_SetResetStore = 2152;
        public const int Btn_SetCloseStore = 2153;
        public const int Btn_SetDeleteStore = 2257;
        public const int Btn_SetViewStore = 2256;

        // =====================
        // Module: Zone Management
        // =====================

        // Buttons
        public const int Btn_SetSaveZone = 2154;
        public const int Btn_SetEditZone = 2155;
        public const int Btn_SetHelpZone = 2156;
        public const int Btn_SetResetZone = 2157;
        public const int Btn_SetCloseZone = 2158;
        public const int Btn_SetViewZone = 2259;
        public const int Btn_SetDeleteZone = 2260;

        // =====================
        // Module: Rack Management
        // =====================

        // Buttons
        public const int Btn_SetAddRack = 2159;
        public const int Btn_SetReportRack = 2160;
        public const int Btn_SetResetRack = 2161;
        public const int Btn_SetEditRack = 2162;
        public const int Btn_SetSaveRack = 2163;
        public const int Btn_SetViewRack = 2261;
        public const int Btn_SetDeleteRack = 2262;

        // =====================
        // Module: Item Management
        // =====================
    //    public const int Mnu_Item = 1045;

        // Tabs
        public const int Tab_SetItemItems = 2164;
        public const int Tab_SetForApprovaltems = 2165;
        public const int Tab_SetItemDetails1 = 2171;
        public const int Tab_SetItemDetails2 = 2172;

        // Buttons
        public const int Btn_SetAddItems = 2166;
        public const int Btn_SetEditItems = 2167;
        public const int Btn_SetReportItems = 2168;
        public const int Btn_SetResetItems = 2169;
        public const int Btn_SetApprovalItems = 2170;
        public const int Btn_SetSaveItems = 2173;
        public const int Btn_SetViewItem = 2263;
        public const int Btn_SetDeleteItem = 2264;

        // =====================
        // Module: ItemType Management
        // =====================
    //    public const int Mnu_ItemType = 1044;

        // Buttons
        public const int Btn_SetSaveItemType = 2174;
        public const int Btn_SetEditItemType = 2175;
        public const int Btn_SetHelpItemType = 2176;
        public const int Btn_SetCloseItemType = 2177;
        public const int Btn_SetViewItemType = 2265;
        public const int Btn_SetDeleteItemType = 2266;

        // =====================
        // Module: Compatible Items
        // =====================

        // Tabs
        public const int Tab_SetCompatibleItems = 2178;
        public const int Tab_SetAddCompatibleItems = 2179;

        // Buttons
        public const int Btn_SetSaveCompatibleItems = 2180;
        public const int Btn_SetReportCompatibleItems = 2181;
        public const int Btn_SetResetCompatibleItems = 2182;
        public const int Btn_SetEditCompatibleItems = 2183;
        public const int Btn_SetViewCompatibleItems = 2267;
        public const int Btn_SetDeleteCompatibleItems = 2268;

        // =====================
        // Module: Brand Management
        // =====================
   //     public const int Mnu_Brand = 1046;

        // Buttons
        public const int Btn_SetSaveBrand = 2203;
        public const int Btn_SetEditBrand = 2204;
        public const int Btn_SetHelpBrand = 2205;
        public const int Btn_SetCloseBrand = 2206;
        public const int Btn_SetViewBrand = 2273;
        public const int Btn_SetDeleteBrand = 2274;

        // =====================
        // Module: BrandItemType Management
        // =====================

        // Buttons
        public const int Btn_SetSaveBrandItemType = 2207;
        public const int Btn_SetEditBrandItemType = 2208;
        public const int Btn_SetHelpBrandItemType = 2209;
        public const int Btn_SetCloseBrandItemType = 2210;
        public const int Btn_SetViewBrandItemType = 2275;
        public const int Btn_SetDeleteBrandItemType = 2276;

        // =====================
        // Module: Model Management
        // =====================
    //    public const int Mnu_Model = 1047;

        // Buttons
        public const int Btn_SetSaveModel = 2211;
        public const int Btn_SetEditModel = 2212;
        public const int Btn_SetHelpModel = 2213;
        public const int Btn_SetResetModel = 2214;
        public const int Btn_SetCloseModel = 2215;
        public const int Btn_SetViewModel = 2277;
        public const int Btn_SetDeleteModel = 2278;

        // =====================
        // Module: Feeding Mechanism
        // =====================

        // Buttons
        public const int Btn_SetSaveFeedingMechanism = 2216;
        public const int Btn_SetEditFeedingMechanism = 2217;
        public const int Btn_SetHelpFeedingMechanism = 2218;
        public const int Btn_SetCloseFeedingMechanism = 2219;
        public const int Btn_SetViewFeedingMechanism = 2279;
        public const int Btn_SetDeleteFeedingMechanism = 2280;

        // =====================
        // Module: BedType Management
        // =====================
      //  public const int Mnu_BedType = 1055;

        // Buttons
        public const int Btn_SetSaveBedType = 2220;
        public const int Btn_SetEditBedType = 2221;
        public const int Btn_SetHelpBedType = 2222;
        public const int Btn_SetCloseBedType = 2223;
        public const int Btn_SetViewBedType = 2281;
        public const int Btn_SetDeleteBedType = 2282;

        // =====================
        // Module: Customer Price Category
        // =====================

        // Buttons
        public const int Btn_SetSaveCusPriceCategory = 2224;    // Parent 2133
        public const int Btn_SetEditCusPriceCategory = 2225;    // Parent 2133
        public const int Btn_SetHelpCusPriceCategory = 2226;    // Parent 2133
        public const int Btn_SetCloseCusPriceCategory = 2227;   // Parent 2133
        public const int Btn_SetViewCusPriceCategory = 2283;    // Parent 2133
        public const int Btn_SetDeleteCusPriceCategory = 2284;  // Parent 2133

        // =====================
        // Module: Asset Type
        // =====================
     //   public const int Mnu_AssetType = 1057;

        // Buttons
        public const int Btn_SetSaveAssetType = 2228;
        public const int Btn_SetEditAssetType = 2229;
        public const int Btn_SetHelpAssetType = 2230;
        public const int Btn_SetResetAssetType = 2231;
        public const int Btn_SetCloseAssetType = 2232;
        public const int Btn_SetViewAssetType = 2285;
        public const int Btn_SetDeleteAssetType = 2286;

        // =====================
        // Module: Asset SubType
        // =====================
     //   public const int Mnu_AssetSubType = 1058;

        // Buttons
        public const int Btn_SetSaveAssetSubType = 2233;
        public const int Btn_SetEditAssetSubType = 2234;
        public const int Btn_SetHelpAssetSubType = 2235;
        public const int Btn_SetResetAssetSubType = 2236;
        public const int Btn_SetCloseAssetSubType = 2237;
        public const int Btn_SetViewAssetSubType = 2287;
        public const int Btn_SetDeleteAssetSubType = 2288;

        // =====================
        // Module: Attachment
        // =====================

        // Buttons
        public const int Btn_SetSaveAttachment = 2238;
        public const int Btn_SetEditAttachment = 2239;
        public const int Btn_SetHelpAttachment = 2240;
        public const int Btn_SetCloseAttachment = 2241;
        public const int Btn_SetViewAttachment = 2289;
        public const int Btn_SetDeleteAttachment = 2290;

        // =====================
        // Module: SKU Bin Location
        // =====================
    //    public const int Mnu_SKUBinLocation = 1042;

        // Buttons
        public const int Btn_SetSaveSKUBinLoaction = 2249;
        public const int Btn_SetEditSKUBinLoaction = 2250;
        public const int Btn_SetHelpSKUBinLoaction = 2251;
        public const int Btn_SetResetSKUBinLoaction = 2252;
        public const int Btn_SetCloseSKUBinLoaction = 2253;
        public const int Btn_SetDeleteSKUBinLoaction = 2254;
        public const int Btn_SetViewSKUBinLoaction = 2255;
        public const int Btn_SetAddSKUBinLocation = 2291;
        public const int Btn_SetReportSKUBinLocation = 2258;

        // =====================
        // Module: FP Scanners
        // =====================

        // Tabs
        public const int Tab_SetFpScannersDetails = 2420;
        public const int Tab_SetFpScannersSchedule = 2421;

        // Buttons
        public const int Btn_SetFpScannersAdd = 2422;
        public const int Btn_SetFpScannersReport = 2423;
        public const int Btn_SetFpScannersReset = 2424;
        public const int Btn_SetFpScannersEdit = 2425;
        public const int Btn_SetFpScannersDelete = 2426;
        public const int Btn_SetFpScannersSave = 2427;
        public const int Btn_SetFpScannersView = 2428;
        public const int Btn_SetFpScannersHelp = 2429;
        public const int Btn_SetFpScannersClose = 2430;
        public const int Btn_SetScheduleAdd = 2431;
        public const int Btn_SetScheduleEdit = 2432;
        public const int Btn_SetScheduleDelete = 2433;

        // =====================
        // Module: Divisional Section
        // =====================

        // Buttons
        public const int Btn_SetDivisionalSecAdd = 2434;
        public const int Btn_SetDivisionalSecReport = 2435;
        public const int Btn_SetDivisionalSecReset = 2436;
        public const int Btn_SetDivisionalSecView = 2437;
        public const int Btn_SetDivisionalSecEdit = 2438;
        public const int Btn_SetDivisionalSecDelete = 2439;
        public const int Btn_SetDivisionalSecSave = 2440;

        // Module Setup - Gramaniladari
        public const int Btn_SetGramaniladariAdd = 2441; // parent1075 // mnu level3 // ModuleSetup
        public const int Btn_SetGramaniladariReport = 2442; // parent1075 // mnu level3 // ModuleSetup
        public const int Btn_SetGramaniladariReset = 2443; // parent1075 // mnu level3 // ModuleSetup
        public const int Btn_SetGramaniladariView = 2444; // parent1075 // mnu level3 // ModuleSetup
        public const int Btn_SetGramaniladariEdit = 2445; // parent1075 // mnu level3 // ModuleSetup
        public const int Btn_SetGramaniladariDelete = 2446; // parent1075 // mnu level3 // ModuleSetup
        public const int Btn_SetGramaniladariSave = 2447; // parent1075 // mnu level3 // ModuleSetup

        // Module Setup - Asset Items
        public const int Btn_AssetItemsAdd = 2527; // parent2526 // mnu level3 // ModuleSetup
        public const int Btn_AssetItemsEdit = 2528; // parent2526 // mnu level3 // ModuleSetup
        public const int Btn_AssetItemsDelete = 2529; // parent2526 // mnu level3 // ModuleSetup
        public const int Btn_AssetItemsSave = 2530; // parent2526 // mnu level3 // ModuleSetup
        public const int Tab_AssetItemDetails = 2531; // parent2526 // mnu level3 // ModuleSetup
        public const int Tab_ComponetsAndAttachements = 2532; // parent2526 // mnu level3 // ModuleSetup
        public const int Btn_AssetItemsReport = 2533; // parent2526 // mnu level3 // ModuleSetup
        public const int Btn_AssetItemsReset = 2534; // parent2526 // mnu level3 // ModuleSetup
        public const int Btn_AssetItemsHelp = 2670; // parent2526 // mnu level3 // ModuleSetup
        public const int Btn_AssetItemsClose = 2671; // parent2526 // mnu level3 // ModuleSetup
        public const int Btn_AssetItemsView = 2672; // parent2526 // mnu level3 // ModuleSetup

        // Module Setup - Police Station
        public const int Btn_SetPoliceStationAdd = 2537; // parent1077 // mnu level3 // ModuleSetup
        public const int Btn_SetPoliceStationReport = 2538; // parent1077 // mnu level3 // ModuleSetup
        public const int Btn_SetPoliceStationReset = 2539; // parent1077 // mnu level3 // ModuleSetup
        public const int Btn_SetPoliceStationView = 2540; // parent1077 // mnu level3 // ModuleSetup
        public const int Btn_SetPoliceStationEdit = 2541; // parent1077 // mnu level3 // ModuleSetup
        public const int Btn_SetPoliceStationDelete = 2542; // parent1077 // mnu level3 // ModuleSetup
        public const int Btn_SetPoliceStationSave = 2543; // parent1077 // mnu level3 // ModuleSetup

        // Module Setup - Bank Branch
        public const int Btn_SetBankBranchAdd = 2544; // parent1088 // mnu level3 // ModuleSetup
        public const int Btn_SetBankBranchReport = 2545; // parent1088 // mnu level3 // ModuleSetup
        public const int Btn_SetBankBranchReset = 2546; // parent1088 // mnu level3 // ModuleSetup
        public const int Btn_SetBankBranchEdit = 2547; // parent1088 // mnu level3 // ModuleSetup
        public const int Btn_SetBankBranchView = 2548; // parent1088 // mnu level3 // ModuleSetup
        public const int Btn_SetBankBranchSave = 2549; // parent1088 // mnu level3 // ModuleSetup
        public const int Btn_SetBankBranchDelete = 2632; // parent1088 // mnu level3 // ModuleSetup

        // Module Setup - Bank
        public const int Btn_SetBankView = 3110; // parent1090 // mnu level3 // ModuleSetup
        public const int Btn_SetBankSave = 2550; // parent1090 // mnu level3 // ModuleSetup
        public const int Btn_SetBankEdit = 2551; // parent1090 // mnu level3 // ModuleSetup
        public const int Btn_SetBankHelp = 2552; // parent1090 // mnu level3 // ModuleSetup
        public const int Btn_SetBankClose = 2553; // parent1090 // mnu level3 // ModuleSetup
        public const int Btn_SetBankDelete = 2554; // parent1090 // mnu level3 // ModuleSetup

        // Module Setup - Polling Division
        public const int Btn_SetPollingDivisionAdd = 2576; // parent2575 // mnu level3 // ModuleSetup
        public const int Btn_SetPollingDivisionReport = 2577; // parent2575 // mnu level3 // ModuleSetup
        public const int Btn_SetPollingDivisionReset = 2578; // parent2575 // mnu level3 // ModuleSetup
        public const int Btn_SetPollingDivisionView = 2579; // parent2575 // mnu level3 // ModuleSetup
        public const int Btn_SetPollingDivisionEdit = 2580; // parent2575 // mnu level3 // ModuleSetup
        public const int Btn_SetPollingDivisionDelete = 2581; // parent2575 // mnu level3 // ModuleSetup
        public const int Btn_SetPollingDivisionSave = 2582; // parent2575 // mnu level3 // ModuleSetup//

        // Module Setup - Department
        public const int Btn_SetDepartmentSave = 2583; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetDepartmentHelp = 2584; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetDepartmentClose = 2585; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetDepartmentDelete = 2586; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetDepartmentView = 2674; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetDepartmentEdit = 3090; // parent1081 // mnu level3 // ModuleSetup

        // Module Setup - District
        public const int Btn_SetSaveDistrict = 3125; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpDistrict = 3129; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteDistrict = 3127; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetViewDistrict = 3124; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetEditDistrict = 3126; // parent1081 // mnu level3 // ModuleSetup

        //Ethnicity
       
        public const int Btn_SetSaveEthnicity = 3145; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpEthnicity = 3148; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteEthnicity = 3147; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetViewEthnicity = 3144; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetEditEthnicity = 3146; // parent1081 // mnu level3 // ModuleSetup


        // Module Setup - Province
        public const int Btn_SetSaveProvince = 3135; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpProvince = 3138; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteProvince = 3137; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetViewProvince = 3134; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetEditProvince = 3136; // parent1081 // mnu level3 // ModuleSetup

        // Module Setup - Religion
        public const int Btn_SetSaveReligion = 3140; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpReligion = 3143; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteReligion = 3142; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetViewReligion = 3139; // parent1081 // mnu level3 // ModuleSetup
        public const int Btn_SetEditReligion = 3141; // parent1081 // mnu level3 // ModuleSetup


        // Module Setup - Job Title
        public const int Btn_SetJobTitleSave = 2588; // parent2587 // mnu level3 // ModuleSetup
        public const int Btn_SetJobTitleHelp = 2589; // parent2587 // mnu level3 // ModuleSetup
        public const int Btn_SetJobTitleClose = 2590; // parent2587 // mnu level3 // ModuleSetup
        public const int Btn_SetJobTitleDelete = 2591; // parent2587 // mnu level3 // ModuleSetup
        public const int Btn_SetJobTitleView = 2675; // parent2587 // mnu level3 // ModuleSetup
        public const int Btn_SetJobTitleEdit = 3111; // parent2587 // mnu level3 // ModuleSetup

        // Module Setup - Designation
        public const int Btn_SetDesignationSave = 2592; // parent1060 // mnu level3 // ModuleSetup
        public const int Btn_SetDesignationEdit = 2593; // parent1060 // mnu level3 // ModuleSetup
        public const int Btn_SetDesignationHelp = 2594; // parent1060 // mnu level3 // ModuleSetup
        public const int Btn_SetDesignationClose = 2595; // parent1060 // mnu level3 // ModuleSetup
        public const int Btn_SetDesignationDelete = 2596; // parent1060 // mnu level3 // ModuleSetup
        public const int Btn_SetDesignationView = 2676; // parent1060 // mnu level3 // ModuleSetup

        // Module Setup - Section
        public const int Btn_SetSectionView = 3093; // parent1082 // mnu level3 // ModuleSetup
        public const int Btn_SetSectionEdit = 3092; // parent1082 // mnu level3 // ModuleSetup
        public const int Btn_SetSectionSave = 2597; // parent1082 // mnu level3 // ModuleSetup
        public const int Btn_SetSectionHelp = 2598; // parent1082 // mnu level3 // ModuleSetup
        public const int Btn_SetSectionClose = 2599; // parent1082 // mnu level3 // ModuleSetup
        public const int Btn_SetSectionDelete = 2600; // parent1082 // mnu level3 // ModuleSetup

        // Module Setup - Nationality
        public const int Btn_SetNationalityView = 3095; // parent1064 // mnu level3 // ModuleSetup
        public const int Btn_SetNationalityEdit = 3094; // parent1064 // mnu level3 // ModuleSetup
        public const int Btn_SetNationalitySave = 2601; // parent1064 // mnu level3 // ModuleSetup
        public const int Btn_SetNationalityHelp = 2602; // parent1064 // mnu level3 // ModuleSetup
        public const int Btn_SetNationalityClose = 2603; // parent1064 // mnu level3 // ModuleSetup
        public const int Btn_SetNationalityDelete = 2604; // parent1064 // mnu level3 // ModuleSetup

        // Module Setup - Delete Popup
        public const int Btn_SetSaveDeletePopup = 2629; // parent2628 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpDeletePopup = 2630; // parent2628 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseDeletePopup = 2631; // parent2628 // mnu level3 // ModuleSetup

        // Module Setup - Holiday Calendar
        public const int Tab_SetHolidayDetails = 2678; // parent2677 // mnu level3 // ModuleSetup
        public const int Tab_SetSpecialHolidays = 2679; // parent2677 // mnu level3 // ModuleSetup
        public const int Tab_SetCoveringDays = 2680; // parent2677 // mnu level3 // ModuleSetup
        public const int Btn_SetAddCalendar = 2681; // parent2677 // mnu level3 // ModuleSetup
        public const int Btn_SetReportCalendar = 2682; // parent2677 // mnu level3 // ModuleSetup
        public const int Btn_SetResetCalendar = 2683; // parent2677 // mnu level3 // ModuleSetup
        public const int Btn_SetEditCalendar = 2684; // parent2677 // mnu level3 // ModuleSetup
        public const int Btn_SetSaveCalendar = 2685; // parent2677 // mnu level3 // ModuleSetup
        public const int Btn_SetViewCalendar = 2686; // parent2677 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteCalendar = 2687; // parent2677 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpCalendar = 2688; // parent2677 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseCalendar = 2689; // parent2677 // mnu level3 // ModuleSetup

        // Dashboard - Example
        public const int Mnu_Dashboard = 1124; // parent0 // mnu level0 // ModuleDashboard
        public const int Mnu_DashboardHRMandPayroll = 3041; // parent1124 // mnu level1 // ModuleDashboard
        public const int Mnu_DashboardGatePassManage = 3042; // parent1124 // mnu level1 // ModuleDashboard
        public const int Tab_DepartmentCount = 3043; // parent3041 // mnu level2 // ModuleDashboard
        public const int Tab_UpcomingBirthdays = 3044; // parent3041 // mnu level2 // ModuleDashboard
        public const int Tab_ProbationCompletion = 3045; // parent3041 // mnu level2 // ModuleDashboard
        public const int Tab_EmployeeTurnOver = 3046; // parent3041 // mnu level2 // ModuleDashboard
        public const int Tab_LeaveSummary = 3047; // parent3041 // mnu level2 // ModuleDashboard
        public const int Tab_EmployeeGateass = 3048; // parent3042 // mnu level2 // ModuleDashboard
        public const int Tab_EmployeeGatePassCount = 3049; // parent3042 // mnu level2 // ModuleDashboard
        public const int Tab_DriverGatepass = 3050; // parent3042 // mnu level2 // ModuleDashboard
        public const int Tab_DriverGatePassCount = 3051; // parent3042 // mnu level2 // ModuleDashboard
        public const int Tab_VisitorGatepass = 3052; // parent3042 // mnu level2 // ModuleDashboard
        public const int Tab_VisitorGatePassCount = 3053; // parent3042 // mnu level2 // ModuleDashboard

        // Module: Setup

        // Qualification Type
        public const int Btn_SetViewQualificationType = 3098; // parent2690 // mnu level3 // ModuleSetup
        public const int Btn_SetEditQualificationType = 3099; // parent2690 // mnu level3 // ModuleSetup

        public const int Btn_SetSaveQualificationType = 2691; // parent2690 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteQualificationType = 2692; // parent2690 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpQualificationType = 2693; // parent2690 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseQualificationType = 2720; // parent2690 // mnu level3 // ModuleSetup

        // Building
        public const int Btn_SetViewBuilding = 3096; // parent1083 // mnu level3 // ModuleSetup
        public const int Btn_SetEditBuilding = 3097; // parent1083 // mnu level3 // ModuleSetup
        public const int Btn_SetSaveBuilding = 2781; // parent1083 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpBuilding = 2782; // parent1083 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteBuilding = 2783; // parent1083 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseBuilding = 2784; // parent1083 // mnu level3 // ModuleSetup

        // Floor
        public const int Btn_SetSaveFloor = 2785; // parent1084 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpFloor = 2786; // parent1084 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteFloor = 2787; // parent1084 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseFloor = 2788; // parent1084 // mnu level3 // ModuleSetup
        public const int Btn_SetEditFloor = 3100; // parent1084 // mnu level3 // ModuleSetup
        public const int Btn_SetViewFloor = 3101; // parent1084 // mnu level3 // ModuleSetup


        // Production Line
        public const int Btn_SetViewProductionLine = 3102; // parent1085 // mnu level3 // ModuleSetup
        public const int Btn_SetEditProductionLine = 3106; // parent1085 // mnu level3 // ModuleSetup
        public const int Btn_SetSaveProductionLine = 2789; // parent1085 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpProductionLine = 2790; // parent1085 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteProductionLine = 2791; // parent1085 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseProductionLine = 2792; // parent1085 // mnu level3 // ModuleSetup

        // MOH
        public const int Btn_SetMOHAdd = 2839; // parent1076 // mnu level3 // ModuleSetup
        public const int Btn_SetMOHReport = 2840; // parent1076 // mnu level3 // ModuleSetup
        public const int Btn_SetMOHReset = 2841; // parent1076 // mnu level3 // ModuleSetup
        public const int Btn_SetMOHSave = 2842; // parent1076 // mnu level3 // ModuleSetup
        public const int Btn_SetMOHEdit = 2843; // parent1076 // mnu level3 // ModuleSetup
        public const int Btn_SetMOHDelete = 2844; // parent1076 // mnu level3 // ModuleSetup
        public const int Btn_SetMOHView = 3107; // parent1076 // mnu level3 // ModuleSetup

        // Reason
        public const int Mnu_SetReasonSave = 2846; // parent2845 // mnu level3 // ModuleSetup
        public const int Mnu_SetReasonReset = 2847; // parent2845 // mnu level3 // ModuleSetup
        public const int Mnu_SetReasonHelp = 2848; // parent2845 // mnu level3 // ModuleSetup
        public const int Mnu_SetReasonClose = 2849; // parent2845 // mnu level3 // ModuleSetup

        // Deduction
        public const int Btn_SetDeductionSave = 2851; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_SetDeductionView = 2852; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_SetDeductionAdd = 2922; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_SetDeductionReport = 2923; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_SetDeductionReset = 2924; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_SetDeductionEdit = 2925; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_SetDeductionDelete = 2926; // parent2850 // mnu level3 // ModuleSetup


        // Deduction Entry or Employee Deduction
        public const int Btn_DeductionEntrySave = 2947; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_DeductionEntryView = 2946; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_DeductionEntryAdd = 2941; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_DeductionEntryReport = 2942; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_DeductionEntryReset = 2943; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_DeductionEntryEdit = 2945; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_DeductionEntryDelete = 2948; // parent2850 // mnu level3 // ModuleSetup
        public const int Btn_DeductionEntryCalculate = 3160;
        public const int Btn_DeductionEntryAction =2944;

        // Allowances
        public const int Btn_SetAllowancesAdd = 2855; // parent2854 // mnu level3 // ModuleSetup
        public const int Btn_SetAllowancesReport = 2856; // parent2854 // mnu level3 // ModuleSetup
        public const int Btn_SetAllowancesReset = 2857; // parent2854 // mnu level3 // ModuleSetup
        public const int Btn_SetAllowancesEdit = 2858; // parent2854 // mnu level3 // ModuleSetup
        public const int Btn_SetAllowancesSave = 2859; // parent2854 // mnu level3 // ModuleSetup
        public const int Btn_SetAllowancesView = 2920; // parent2854 // mnu level3 // ModuleSetup
        public const int Btn_SetAllowancesDelete = 2921; // parent2854 // mnu level3 // ModuleSetup

        // Grade
        public const int Btn_SetSaveGrade = 2881; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpGrade = 2882; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseGrade = 2883; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteGrade = 2884; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetViewGrade = 3114; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetEitGrade = 3113; // parent1061 // mnu level3 // ModuleSetup

        // Skill Level
        public const int Btn_SetSaveSkillLevel = 2886; // parent2885 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpSkillLevel = 2887; // parent2885 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseSkillLevel = 2888; // parent2885 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteSkillLevel = 2889; // parent2885 // mnu level3 // ModuleSetup
        public const int Btn_SetViewSkillLevel = 3131; // parent2885 // mnu level3 // ModuleSetup
        public const int Btn_SetEditSkillLevel = 3133; // parent2885 // mnu level3 // ModuleSetup


        // Transport Route
        public const int Btn_SetAddTransportRoute = 2893; // parent2892 // mnu level3 // ModuleSetup
        public const int Btn_SetReportTransportRoute = 2894; // parent2892 // mnu level3 // ModuleSetup
        public const int Btn_SetResetTransportRoute = 2895; // parent2892 // mnu level3 // ModuleSetup
        public const int Btn_SetSaveTransportRoute = 2896; // parent2892 // mnu level3 // ModuleSetup
        public const int Btn_SetEditTransportRoute = 2897; // parent2892 // mnu level3 // ModuleSetup
        public const int Btn_SetViewTransportRoute = 2898; // parent2892 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteTransportRoute = 2899; // parent2892 // mnu level3 // ModuleSetup

        // Emolument
        public const int Btn_SetEmolumentAdd = 2913; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_SetEmolumentEdit = 2914; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_SetEmolumentSave = 2915; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_SetEmolumentReport = 2916; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_SetEmolumentReset = 2917; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_SetEmolumentView = 2918; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_SetEmolumentDelete = 2919; // parent2912 // mnu level3 // ModuleSetup

        // Emolument Entry or Employee Emolument
        public const int Btn_EmolumentEntryAdd = 2930; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_EmolumentEntryEdit = 2934; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_EmolumentEntrySave = 2936; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_EmolumentEntryReport = 2931; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_EmolumentEntryReset = 2932; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_EmolumentEntryView = 2935; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_EmolumentEntryDelete = 2937; // parent2912 // mnu level3 // ModuleSetup
        public const int Btn_EmolumentEntryCalculate = 3158;

        // PAYE Tax Upload
        public const int Btn_SetViewPayeTaxUpload = 3115; // parent2949 // mnu level3 // ModuleSetup
        public const int Btn_SetAddPayeTaxUpload = 2950; // parent2949 // mnu level3 // ModuleSetup
        public const int Btn_SetReportPayeTaxUpload = 2951; // parent2949 // mnu level3 // ModuleSetup
        public const int Btn_SetResetPayeTaxUpload = 2952; // parent2949 // mnu level3 // ModuleSetup
        public const int Btn_SetSavePayeTaxUpload = 2953; // parent2949 // mnu level3 // ModuleSetup
        public const int Btn_SetEditPayeTaxUpload = 2954; // parent2949 // mnu level3 // ModuleSetup
        public const int Btn_SetDeletePayeTaxUpload = 2955; // parent2949 // mnu level3 // ModuleSetup
        public const int Tab_SetPayeTaxUploadDetails = 2956; // parent2949 // mnu level3 // ModuleSetup
        public const int Tab_SetExcelTableUpload = 2957; // parent2949 // mnu level3 // ModuleSetup
        public const int Btn_SetImportPayeTaxUpload = 2958; // parent2949 // mnu level3 // ModuleSetup

        // Open Payroll Period
        public const int Btn_SetAddOpenPayrollPeriod = 2960; // parent2959 // mnu level3 // ModuleSetup
        public const int Btn_SetReportOpenPayrollPeriod = 2961; // parent2959 // mnu level3 // ModuleSetup
        public const int Btn_SetResetOpenPayrollPeriod = 2962; // parent2959 // mnu level3 // ModuleSetup
        public const int Btn_SetSaveOpenPayrollPeriod = 2963; // parent2959 // mnu level3 // ModuleSetup
        public const int Btn_SetOpenPayrollPeriod = 2964; // parent2959 // mnu level3 // ModuleSetup

        // Emolument Group
        public const int Btn_SetViewEmolumentGroup = 3116; // parent3016 // mnu level3 // ModuleSetup
        public const int Btn_SetSaveEmolumentGroup = 3024; // parent3016 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteEmolumentGroup = 3025; // parent3016 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpEmolumentGroup = 3026; // parent3016 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseEmolumentGroup = 3027; // parent3016 // mnu level3 // ModuleSetup

        // Deduction Group
        public const int Btn_SetSaveDeductionGroup = 3028; // parent3015 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteDeductionGroup = 3029; // parent3015 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpDeductionGroup = 3030; // parent3015 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseDeductionGroup = 3031; // parent3015 // mnu level3 // ModuleSetup
        public const int Btn_SetViewDeductionGroup = 3149; // parent3015 // mnu level3 // ModuleSetup
        public const int Btn_SetEditDeductionGroup = 3150; // parent3015 // mnu level3 // ModuleSetup

        // Drivers
        public const int Btn_SetAddDrivers = 3056; // parent3055 // mnu level3 // ModuleSetup
        public const int Btn_SetReportDrivers = 3057; // parent3055 // mnu level3 // ModuleSetup
        public const int Btn_SetResetDrivers = 3058; // parent3055 // mnu level3 // ModuleSetup
        public const int Btn_SetSaveDrivers = 3059; // parent3055 // mnu level3 // ModuleSetup
        public const int Btn_SetEditDrivers = 3060; // parent3055 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteDrivers = 3061; // parent3055 // mnu level3 // ModuleSetup
        public const int Btn_SetViewDrivers = 3112; // parent3055 // mnu level3 // ModuleSetup

        // Vehicles
        public const int Tab_SetVehicles = 3062; // parent3021 // mnu level3 // ModuleSetup
        public const int Tab_SetVehicleRunningRecord = 3063; // parent3021 // mnu level3 // ModuleSetup
        public const int Btn_SetAddVehicles = 3064; // parent3021 // mnu level3 // ModuleSetup
        public const int Btn_SetReportVehicles = 3065; // parent3021 // mnu level3 // ModuleSetup
        public const int Btn_SetResetVehicles = 3066; // parent3021 // mnu level3 // ModuleSetup
        public const int Btn_SetSaveVehicles = 3067; // parent3021 // mnu level3 // ModuleSetup
        public const int Btn_SetViewVehicles = 3068; // parent3021 // mnu level3 // ModuleSetup
        public const int Btn_SetEditVehicles = 3069; // parent3021 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteVehicles = 3070; // parent3021 // mnu level3 // ModuleSetup
        public const int Btn_SetRunningRecords = 3071; // parent3021 // mnu level3 // ModuleSetup



        // Module: Message
        public const int Mnu_Messages = 2297; // parent0 // mnu level0 // ModuleMessage

        // Sub-Menu: User Messages
        public const int Mnu_UserMessages = 2298; // parent2297 // mnu level1 // ModuleMessage
        // Buttons under User Messages
        public const int Btn_AddMessages = 2300; // parent2298 // mnu level2 // ModuleMessage
        public const int Btn_SendMessages = 2301; // parent2298 // mnu level2 // ModuleMessage
        public const int Btn_ResetMessages = 2307; // parent2298 // mnu level2 // ModuleMessage
        // Tabs under User Messages
        public const int Tab_UserMessages = 2302; // parent2298 // mnu level2 // ModuleMessage

        // Sub-Menu: System Messages
        public const int Mnu_SystemMessages = 2299; // parent2297 // mnu level1 // ModuleMessage
        // Tabs under System Messages
        public const int Tab_SystemMessages = 2303; // parent2299 // mnu level2 // ModuleMessage


        // Module: Visitor
        public const int Mnu_Visitor = 2970; // parent0 // mnu level0 // ModuleVisitor

        // Sub-Menu: Gate Pass Manage
        public const int Mnu_GatePassManage = 2971; // parent2970 // mnu level1 // ModuleVisitor
        // Buttons under Gate Pass Manage
        public const int Btn_AddGatePass = 2974; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_ReportGatePass = 2975; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_ResetGatePass = 2976; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_SaveGatePass = 2977; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_EditGatePass = 2978; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_ViewGatePass = 2979; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_ConfirmGatePass = 2980; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_ApprovalGatePass = 2981; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_InGatePass = 2982; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_OutGatePass = 2983; // parent2971 // mnu level2 // ModuleVisitor
        public const int Btn_DeleteGatePass = 3032; // parent2971 // mnu level2 // ModuleVisitor
        // Tabs under Gate Pass Manage
        public const int Tab_GatePass = 3033; // parent2971 // mnu level2 // ModuleVisitor
        public const int Tab_GatePassForApproval = 3034; // parent2971 // mnu level2 // ModuleVisitor
        public const int Tab_GatePassForConfirmation = 3035; // parent2971 // mnu level2 // ModuleVisitor

        // Sub-Menu: Visitor Manage
        public const int Mnu_VisitorManage = 2972; // parent2970 // mnu level1 // ModuleVisitor
        // Buttons under Visitor Manage
        public const int Btn_AddVisitorManage = 2984; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_ReportVisitorManage = 2985; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_ResetVisitorManage = 2986; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_SaveVisitorManage = 2987; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_EditVisitorManage = 2988; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_ViewVisitorManage = 2989; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_ConfirmVisitorManage = 2990; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_ApprovalVisitorManage = 2991; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_InVisitorManage = 2992; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_OutVisitorManage = 2993; // parent2972 // mnu level2 // ModuleVisitor
        public const int Btn_DeleteVisitorManage = 3004; // parent2972 // mnu level2 // ModuleVisitor
        // Tabs under Visitor Manage
        public const int Tab_VisitorGatePass = 3036; // parent2972 // mnu level2 // ModuleVisitor
        public const int Tab_VisitorGatePassForApproval = 3037; // parent2972 // mnu level2 // ModuleVisitor

        // Sub-Menu: Driver Manage
        public const int Mnu_DriverManage = 2973; // parent2970 // mnu level1 // ModuleVisitor
        // Buttons under Driver Manage
        public const int Btn_AddDriverManage = 2994; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_ReportDriverManage = 2995; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_ResetDriverManage = 2996; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_SaveDriverManage = 2997; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_EditDriverManage = 2998; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_ViewDriverManage = 2999; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_ConfirmDriverManage = 3000; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_ApprovalDriverManage = 3001; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_InDriverManage = 3002; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_OutDriverManage = 3003; // parent2973 // mnu level2 // ModuleVisitor
        public const int Btn_DeleteDriverManage = 3038; // parent2973 // mnu level2 // ModuleVisitor
        // Tabs under Driver Manage
        public const int Tab_DriverGatePass = 3039; // parent2973 // mnu level2 // ModuleVisitor
        public const int Tab_DriverGatePassForApproval = 3040; // parent2973 // mnu level2 // ModuleVisitor
 

        public const int Tab_LeaveEntryEmployees = 2860;
        public const int Tab_Leaves = 2861;
        public const int Tab_ShortLeaveEntryEmployees = 2871;
        public const int Tab_ShortLeaves = 2872;

        // ===============================
        // Leave Entry Buttons
        // ===============================
        public const int Btn_AddLeaveEntry = 2862;
        public const int Btn_ReportLeaveEntry = 2863;
        public const int Btn_ResetLeaveEntry = 2864;
        public const int Btn_SaveLeaveEntry = 2865;
        public const int Btn_EditLeaveEntry = 2866;
        public const int Btn_ViewLeaveEntry = 2867;
        public const int Btn_DeleteLeaveEntry = 2868;
        public const int Btn_UploadLeaveEntry = 2869;
        public const int Btn_ApprovalLeaveEntry = 3022;

        // ===============================
        // Short Leave Entry Buttons
        // ===============================
        public const int Btn_AddShortLeaveEntry = 2873;
        public const int Btn_ReportShortLeaveEntry = 2874;
        public const int Btn_ResetShortLeaveEntry = 2875;
        public const int Btn_SaveShortLeaveEntry = 2876;
        public const int Btn_EditShortLeaveEntry = 2877;
        public const int Btn_ViewShortLeaveEntry = 2878;
        public const int Btn_DeleteShortLeaveEntry = 2879;
        public const int Btn_UploadShortLeaveEntry = 2880;
        public const int Btn_ApproveShortLeaveEntry = 3079;

        // ===============================
        // Leave Application Buttons
        // ===============================
        public const int Btn_AddLeaveApplication = 3073;
        public const int Btn_ReportLeaveApplication = 3074;
        public const int Btn_ResetLeaveApplication = 3075;
        public const int Btn_EditLeaveApplication = 3076;
        public const int Btn_ViewLeaveApplication = 3077;
        public const int Btn_SaveLeaveApplication = 3078;


        // Lieu Leave
        public const int Btn_LieuLeaveView = 3178; // parent3023 // mnu level3 // ModuleSetup
        public const int Btn_LieuLeaveSave = 3179; // parent3023 // mnu level3 // ModuleSetup
        public const int Btn_LieuLeaveEdit = 3180; // parent3023 // mnu level3 // ModuleSetup
        public const int Btn_LieuLeaveDelete = 3181; // parent3023 // mnu level3 // ModuleSetup

        // Over Time
        public const int Btn_OverTimeView = 0; // parent3023 // mnu level3 // ModuleSetup

        // Lieu Leave


        public const int Btn_LeaveEntryApproveView = 0;
        public const int Btn_LeaveEntryApproveSave = 0;
        public const int Btn_LeaveEntryApproveEdit = 0;
        public const int Btn_LeaveEntryApproveDelete = 0;
        public const int Btn_LeaveEntryApproveHelp = 0;

        // Lieu Leave


        public const int Btn_SetViewCoveringDays = 3162;// parent3166 // mnu level3 // ModuleSetup
        public const int Btn_SetSaveCoveringDays = 3163;// parent3166 // mnu level3 // ModuleSetup
        public const int Btn_SetEditCoveringDays = 3164;// parent3166 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteCoveringDays = 3165;// parent3166 // mnu level3 // ModuleSetup

        // Lieu Leave

        public const int Btn_SetViewSpecialHoliday = 3173;
        public const int Btn_SetSaveSpecialHoliday = 3175;
        public const int Btn_SetEditSpecialHoliday = 3176;
        public const int Btn_SetDeleteSpecialHoliday = 3177;

        // Lieu Leave

        public const int Btn_ShortLeaveApproveView = 0;
        public const int Btn_ShortLeaveApproveSave = 0;
        public const int Btn_ShortLeaveApproveEdit = 0;
        public const int Btn_ShortLeaveApproveDelete = 0;
        public const int Btn_ShortLeaveApproveHelp = 0;

        public const int Btn_SetViewCompany = 3183;
        public const int Btn_SetSaveCompany = 3184;
        public const int Btn_SetEditCompany = 3185;
        public const int Btn_SetDeleteCompany = 3186;

        public const int Btn_SetViewCountry = 3187;
        public const int Btn_SetSaveCountry = 3188;
        public const int Btn_SetEditCountry = 3189;
        public const int Btn_SetDeleteCountry = 3190;

        public const int Btn_SetViewCurrency = 3191;
        public const int Btn_SetSaveCurrency = 3192;
        public const int Btn_SetEditCurrency = 3193;
        public const int Btn_SetDeleteCurrency = 3194;


        #region Buttons - UOM & Conversion

        public const int Btn_SetDeleteUOM = 2401;
        public const int Btn_SetViewUOM = 2402;
        public const int Btn_SetDeleteUOMConversion = 2403;
        public const int Btn_SetViewUOMConversion = 2404;

        #endregion

        #region MainCategories
        public const int Btn_SetSaveMainCategory = 2184;
        public const int Btn_SetEditMainCategory = 2185;
        public const int Btn_SetHelpMainCategory = 2186;
        public const int Btn_SetCloseMainCategory = 2187;

        public const int Btn_SetViewMainCategory = 2269;
        public const int Btn_SetDeleteMainCategory = 2270;
        #endregion

        #region SubCategories
        public const int Btn_SetSaveSubCategory = 2188;
        public const int Btn_SetEditSubCategory = 2189;
        public const int Btn_SetHelpSubCategory = 2190;
        public const int Btn_SetResetSubCategory = 2191;
        public const int Btn_SetCloseSubCategory = 2192;

        public const int Btn_SetViewSubCategory = 2271;
        public const int Btn_SetDeleteSubCategory = 2272;
        #endregion

        #region UOM
        public const int Btn_SetSaveUOM = 2193;
        public const int Btn_SetEditUOM = 2194;
        public const int Btn_SetHelpUOM = 2195;
        public const int Btn_SetResetUOM = 2196;
        public const int Btn_SetCloseUOM = 2197;

        #endregion

        #region UOMConversions
        public const int Btn_SetAddUOMConversion = 2198;
        public const int Btn_SetReportUOMConversion = 2199;
        public const int Btn_SetResetUOMConversion = 2200;
        public const int Btn_SetEditUOMConversion = 2201;
        public const int Btn_SetSaveUOMConversion = 2202;
        #endregion

        #region DispatchRentals
        public const int Btn_ViewDispatchRentals = 3196;
        public const int Btn_SaveDispatchRentals = 3197;
        public const int Btn_EditDispatchRentals = 3198;
        public const int Btn_DeleteDispatchRentals = 3199;
        public const int Btn_ApproveDispatchRentals = 3200;
        #endregion

        #region Stock
        public const int Btn_ViewStock = 3203;
        public const int Btn_SaveStock = 3204;
        public const int Btn_EditStock = 3205;
        public const int Btn_DeleteStock = 3206;
        public const int Btn_ApproveStock = 0;
        #endregion

        public const int Btn_RentalAssetAdd = 2319;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_RentalAssetView = 2320;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_RentalAssetDelete = 2321;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_RentalAssetClose = 2322;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_RentalAssetHelp = 2323;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_RentalAssetSave = 1016;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_RentalAssetEdit = 1021;//parent1012//mnu level3//ModuleAsset Management
    
        public const int Btn_IntenalAssetInfo = 1031;//parent6//mnu level3//ModuleAsset Management
        public const int Btn_IntenalAssetSave = 1092;//parent6//mnu level3//ModuleAsset Management

        public const int Btn_AssetMovementRentalView = 2416;//parent2342//mnu level3//ModuleAsset Management

        public const int Btn_AssetMovementInternalView = 2415;//parent2343//mnu level3//ModuleAsset Management

        public const int Btn_AssetMovementInternalSave = 2341;//parent2343//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementInternalEdit = 2338;//parent2343//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementInternalDelete = 2339;//parent2343//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementInternalApproval = 2340;//parent2343//mnu level3//ModuleAsset Management

        public const int Btn_RentalAssetApproval = 2418;//parent1012//mnu level3//ModuleAsset Management




        public const int Mnu_InternalAsst = 6;//parent5//mnu level2//ModuleAsset Management"
        public const int Mnu_RentAsst = 1012;//parent5//mnu level2//ModuleAsset Management
        public const int Tab_RentalAseetDetail1 = 1013;//parent1012//mnu level3//ModuleAsset Management
        public const int Tab_RentalLocation = 1014;//parent1012//mnu level3//ModuleAsset Management
        public const int Tab_RentalAseetDetail2 = 1015;//parent1012//mnu level3//ModuleAsset Management
        public const int Tab_RentalAssetComponents = 1018;//parent1012//mnu level3//ModuleAsset Management
        public const int Tab_RentalAssetAttachments = 1019;//parent1012//mnu level3//ModuleAsset Management
        public const int Tab_RentalAssetFinance1 = 1020;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_RentalAssetReport = 1022;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_RentalAssetReset = 1023;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_RentalAssetExport = 1024;//parent1012//mnu level3//ModuleAsset Management
        public const int Rpt_RentalAssetReport = 1025;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_IntenalAssetAdd = 1026;//parent6//mnu level3//ModuleAsset Management
        public const int Btn_IntenalAssetEdit = 1027;//parent6//mnu level3//ModuleAsset Management
        public const int Btn_IntenalAssetReport = 1028;//parent6//mnu level3//ModuleAsset Management
        public const int Btn_IntenalAssetReset = 1029;//parent6//mnu level3//ModuleAsset Management
        public const int Btn_IntenalAssetClose = 1030;//parent6//mnu level3//ModuleAsset Management
        public const int Btn_IntenalAssetApproval = 1093;//parent6//mnu level3//ModuleAsset Management
        public const int Btn_IntenalAssetAttachment = 1094;//parent6//mnu level3//ModuleAsset Management
        public const int Tab_RentalAssetFinance2 = 2318;//parent1012//mnu level3//ModuleAsset Management
        public const int Tab_IntenalAssetDetail1 = 2324;//parent6//mnu level3//ModuleAsset Management
        public const int Tab_IntenalAssetDetail2 = 2325;//parent6//mnu level3//ModuleAsset Management
        public const int Tab_IntenalAssetLocation = 2326;//parent6//mnu level3//ModuleAsset Management
        public const int Tab_IntenalAssetFinancial1 = 2327;//parent6//mnu level3//ModuleAsset Management
        public const int Tab_IntenalAssetFinancial2 = 2328;//parent6//mnu level3//ModuleAsset Management
        public const int Tab_IntenalAsset = 2329;//parent6//mnu level3//ModuleAsset Management
        public const int Tab_IntenalApproval = 2330;//parent6//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementInternalReport = 2334;//parent2343//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementInternalReset = 2335;//parent2343//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementInternalHelp = 2336;//parent2343//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementInternalClose = 2337;//parent2343//mnu level3//ModuleAsset Management
        public const int Tab_AssetMovementInternalAsset = 2345;//parent2343//mnu level3//ModuleAsset Management
        public const int Tab_AssetMovementInternalApproval = 2346;//parent2343//mnu level3//ModuleAsset Management
        public const int Tab_AssetMovementInternalDetail = 2347;//parent2343//mnu level3//ModuleAsset Management
        public const int Tab_AssetMovementInternalLocation = 2348;//parent2343//mnu level3//ModuleAsset Management
        public const int Tab_AssetMovementRentalTabAsset = 2349;//parent2342//mnu level3//ModuleAsset Management
        public const int Tab_AssetMovementRentalTabApproval = 2350;//parent2342//mnu level3//ModuleAsset Management
        public const int Tab_AssetMovementRentalAssetDetail = 2351;//parent2342//mnu level3//ModuleAsset Management
        public const int Tab_AssetMovementRentalLocation = 2352;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementRentalReport = 2353;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementRentalReset = 2354;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementRentalHelp = 2355;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementRentalClose = 2356;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementRentalEdit = 2357;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementRentalApproval = 2358;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementRentalSave = 2359;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementRentalAdd = 2360;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementRentalDelete = 2399;//parent2342//mnu level3//ModuleAsset Management
        public const int Btn_AssetMovementInternalAdd = 2400;//parent2343//mnu level3//ModuleAsset Management
        public const int Tab_RentalAsset = 2406;//parent1012//mnu level3//ModuleAsset Management
        public const int Tab_RentalAssetForApproval = 2407;//parent1012//mnu level3//ModuleAsset Management
        public const int Btn_IntenalAssetDelete = 2417;//parent6//mnu level3//ModuleAsset Management

        public const int Mnu_JobDescription = 2624;//parent2623//mnu level2//ModuleHRM and Payroll
        public const int Mnu_VacancyNotices = 2625;//parent2623//mnu level2//ModuleHRM and Payroll
        public const int Mnu_OrganizationCharts = 2626;//parent2623//mnu level2//ModuleHRM and Payroll
        public const int Tab_Nominee2 = 2817;//parent2616//mnu level3//ModuleHRM and Payroll
        public const int Tab_Termination = 2891;//parent2806//mnu level3//ModuleHRM and Payroll
        public const int Tab_AllowanceEntryEmployees = 2902;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Tab_Allowances = 2903;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Btn_AllowanceEntryAdd = 2904;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Btn_AllowanceEntryReport = 2905;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Btn_AllowanceEntryReset = 2906;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Btn_AllowanceEntryEdit = 2907;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Btn_AllowanceEntryView = 2908;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Btn_AllowanceEntrySave = 2909;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Btn_AllowanceEntryAction = 2910;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Btn_AllowanceEntryDelete = 2911;//parent2901//mnu level3//ModuleHRM and Payroll
        public const int Tab_EmolumentEntryEmployees = 2928;//parent2927//mnu level3//ModuleHRM and Payroll
        public const int Tab_Emoluments = 2929;//parent2927//mnu level3//ModuleHRM and Payroll
        public const int Tab_DeductionEntryEmployees = 2939;//parent2938//mnu level3//ModuleHRM and Payroll
        public const int Tab_Deductions = 2940;//parent2938//mnu level3//ModuleHRM and Payroll
        public const int Tab_ShiftDetails1 = 2965;//parent2608//mnu level3//ModuleHRM and Payroll
        public const int Tab_ShiftDetails2 = 2966;//parent2608//mnu level3//ModuleHRM and Payroll
        public const int Tab_OTAdjustmentEmployees = 3006;//parent3005//mnu level3//ModuleHRM and Payroll
        public const int Tab_OTAdjustmentEmployeeDetails = 3007;//parent3005//mnu level3//ModuleHRM and Payroll
        public const int Btn_RecalculationOTAdjustment = 3008;//parent3005//mnu level3//ModuleHRM and Payroll
        public const int Btn_ReportOTAdjustment = 3009;//parent3005//mnu level3//ModuleHRM and Payroll
        public const int Btn_ResetOTAdjustment = 3010;//parent3005//mnu level3//ModuleHRM and Payroll
        public const int Btn_ActionOTAdjustment = 3011;//parent3005//mnu level3//ModuleHRM and Payroll
        public const int Btn_SaveOTAdjustment = 3012;//parent3005//mnu level3//ModuleHRM and Payroll
        public const int Btn_ViewOTAdjustment = 3013;//parent3005//mnu level3//ModuleHRM and Payroll
        public const int Btn_EditOTAdjustment = 3014;//parent3005//mnu level3//ModuleHRM and Payroll
        public const int Mnu_AccountsFinance = 1087;//parent0//mnu level0//ModuleAccounts and Finance
        public const int Mnu_Setup = 7;//parent0//mnu level0//ModuleSetup"
        public const int Mnu_Report = 2448;//parent0//mnu level0//ModuleSetup
        public const int Mnu_Masterdata = 1037;//parent7//mnu level1//ModuleSetup
        public const int Tab_ReportCriteria = 2449;//parent2448//mnu level1//ModuleSetup
        public const int Tab_ReportView = 2450;//parent2448//mnu level1//ModuleSetup
        public const int Btn_ReportAdd = 2451;//parent2448//mnu level1//ModuleSetup
        public const int Btn_ReportSave = 2452;//parent2448//mnu level1//ModuleSetup
        public const int Btn_ReportEdit = 2453;//parent2448//mnu level1//ModuleSetup
        public const int Btn_ReportView = 2454;//parent2448//mnu level1//ModuleSetup
        public const int Btn_ReportPrint = 2455;//parent2448//mnu level1//ModuleSetup
        public const int Mnu_SetUser = 1038;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetUserGroup = 1039;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetWarehouse = 1040;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetStore = 1041;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetBinLoaction = 1042;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetItemtype = 1044;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetItem = 1045;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetBrand = 1046;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetModel = 1047;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetMaincategory = 1048;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetSubcategory = 1049;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetUom = 1050;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetUnitconversion = 1051;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetFeedingMechanism = 1053;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetOperationalType = 1054;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetBedType = 1055;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetAssetType = 1057;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetAssetSubtype = 1058;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetDesignation = 1060;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetGrade = 1061;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetReligion = 1063;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetNationality = 1064;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetDivisionalSec = 1074;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetGramaniladari = 1075;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetMOH = 1076;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetPoliceStation = 1077;//parent1037//mnu level2//ModuleSetup
        public const int Mnu_SetAdminGroup = 1079;//parent1037//mnu level2//ModuleSetup

        // Buttons - Asset Ownership
        public const int Mnu_ChangeAssetRegister = 3263;
        public const int Btn_ChangeAssetRegisterSave = 3264;
        public const int Btn_ChangeAssetRegisterReport = 3265;
        public const int Btn_ChangeAssetRegisterReset = 3266;
        public const int Btn_ChangeAssetRegisterClose = 3267;
        public const int Btn_ChangeAssetRegisterHelp = 3268;
        public const int Btn_ChangeAssetRegisterEdit = 3269;
        public const int Btn_ChangeAssetRegisterDelete = 3270;
        public const int Btn_ChangeAssetRegisterAdd = 3271;
        public const int Btn_ChangeAssetRegisterView = 3273;
        public const int Btn_ChangeAssetRegisterApproval = 3274;

        // Buttons - Attach Assets

        public const int Btn_AttachAssetsView = 3235;
        public const int Mnu_AttachAsset = 3217;//parent1037//mnu level2//ModuleSetup

        public const int Mnu_GovStipulated = 3255;
        public const int Btn_SetViewGovStipulated = 3256;
        public const int Btn_SetSaveGovStipulated = 3257;
        public const int Btn_SetEditGovStipulated = 3258;
        public const int Btn_SetViewGovStipulatedClose = 3259;
        public const int Btn_SetViewGovStipulatedHelp = 3260;
        public const int Btn_SetDeleteGovStipulated = 3261;
        public const int Btn_SetViewGovStipulatedAdd = 3262;
        
        // Loans & Salary Advances
        public const int Mnu_LoansSalaryAdvances = 3236;
        public const int Tab_Employee = 3237;
        public const int Tab_LoansAndSalaryAdvances = 3238;
        public const int Btn_LoansAndSalaryAdvancesEdit = 3239;
        public const int Btn_LoansAndSalaryAdvancesView = 3240;
        public const int Btn_LoansAndSalaryAdvancesSkip = 3241;
        public const int Btn_LoansAndSalaryAdvancesAdd = 3242;
        public const int Btn_LoansAndSalaryAdvancesReport = 3243;
        public const int Btn_LoansAndSalaryAdvancesReset = 3244;
        public const int Btn_LoansAndSalaryAdvancesSave = 3254;

        // Incentives Entry
        public const int Mnu_IncentivesEntry = 3245;
        public const int Tab_EmployeeIncentive = 3247;
        public const int Btn_EmployeeIncentAdd = 3248;
        public const int Btn_EmployeeIncentEdit = 3249;
        public const int Btn_EmployeeIncentView = 3250;
        public const int Btn_EmployeeIncentReport = 3251;
        public const int Btn_EmployeeIncentReset = 3252;
        public const int Btn_EmployeeIncentSave = 3253;

        // Incentives Entry
        public const int Mnu_SkipInstallment = 3274;

        public const int Btn_SkipInstallmentSave = 3275;
        public const int Btn_SkipInstallmentEdit = 3276;
        public const int Btn_SkipInstallmentView = 3277;
        public const int Btn_SkipInstallmentAdd = 3278;
        public const int Btn_SkipInstallmentHelp = 3279;
        public const int Btn_SkipInstallmentClose = 3280;

        public const int Mnu_Arrears = 3293;
        public const int Tab_AllArrearsEmployees = 3294;
        public const int Tab_EmployerArrears = 3295;
        public const int Btn_EmployeeArrearsAdd = 3296;
        public const int Btn_EmployeeArrearsEdit = 3297;
        public const int Btn_EmployeeArrearsSave = 3298;
        public const int Btn_EmployeeArrearsHelp = 3299;
        public const int Btn_EmployeeArrearsView = 3300;
        public const int Btn_EmployeeArrearsClose = 3301;

        public const int Mnu_Bonus = 3302;
        public const int Tab_BonusEmployees = 3303;
        public const int Tab_EmployerBonus = 3304;
        public const int Btn_EmployerBonusAdd = 3305;
        public const int Btn_EmployerBonusEdit = 3306;
        public const int Btn_EmployerBonusSave = 3307;
        public const int Btn_EmployerBonusView = 3308;
        public const int Btn_EmployerBonusHelp = 3309;
        public const int Btn_EmployerBonusClose = 3310;

        public const int Mnu_LateMinutesAdjustment = 3283;
        public const int Tab_EmployeeLateMinutesEmployee = 3284;
        public const int Tab_EmployeeLateMinutesDetails = 3285;
        public const int Btn_EmployeeLateMinutesEdit = 3286;
        public const int Btn_EmployeeLateMinutesAdd = 3287;
        public const int Btn_EmployeeLateMinutesSave = 3288;
        public const int Btn_EmployeeLateMinutesDelete = 3289;
        public const int Btn_EmployeeLateMinutesHelp = 3290;
        public const int Btn_EmployeeLateMinutesClose = 3291;
        public const int Btn_LateMinutesAutoProcessin = 3335;

        public const int Mnu_AppointmentLetter = 3292;
        public const int Btn_EmployeeArrearsReset = 3315;
        public const int Btn_EmployeeArrearsReport = 3316;
        public const int Btn_EmployerBonusReport = 3317;
        public const int Btn_EmployerBonusReset = 3318;

        public const int Btn_EmployeeLateMinutesReport = 3311;
        public const int Btn_EmployeeLateMinutesRecalculate = 3312;
        public const int Btn_EmployeeLateMinutesReset = 3313;
        public const int Btn_EmployeeLateMinutesView = 3314;

        public const int Mnu_EmployeeSalary = 3319;
        public const int Btn_EmployeeSalarySave = 3320;
        public const int Btn_EmployeeSalaryView = 3321;
        public const int Btn_EmployeeSalaryEdit = 3322;
        public const int Btn_EmployeeSalaryReport = 3323;
        public const int Btn_EmployeeSalaryReset = 3324;
        public const int Btn_EmployeeSalaryClose = 3325;
        public const int Btn_EmployeeSalaryHelp = 3326;

        public const int Mnu_SalaryProcess = 3327;
        public const int Btn_EmolumentProcess= 3328;
        public const int Btn_DeductionProcess= 3329;
        public const int Btn_SalaryProcess= 3330;
        public const int Btn_SalaryProcessReport= 3331;

        public const int Tab_EmployeeSalary= 3332;
        public const int Tab_EmployerSalaryDetails= 3333;

        public const int Tab_UpcomingEmploymentAnniversary = 3334;

        public const int Mnu_Gratuity= 3336;
        public const int Btn_GratuityAdd= 3337;
        public const int Btn_GratuityEdit= 3338;
        public const int Btn_GratuityView= 3339;
        public const int Btn_GratuitySave= 3340;
        public const int Btn_GratuityReset= 3341;
        public const int Btn_GratuityReport= 3342;
        public const int Btn_GratuityCalculate= 3343;
        public const int Btn_GratuityClose= 3344;

        public const int Btn_SystemPOView = 0;
        public const int Btn_SystemPOSave = 0;
        public const int Btn_SystemPOEdit = 0;
        public const int Btn_SystemPOApprove = 0;
        public const int Btn_SystemPODelete = 0;

        public const int Mnu_RateCard = 3345;
        public const int Tab_RateCard = 3346;
        public const int Tab_RateCardForApproval = 3347;
        public const int Btn_RateCardAdd = 3348;
        public const int Btn_RateCardEdit = 3349;
        public const int Btn_RateCardView = 3350;
        public const int Btn_RateCardSave = 3351;
        public const int Btn_RateCardReset = 3352;
        public const int Btn_RateCardReport = 3353;
        public const int Btn_RateCardClose = 3354;
        public const int Btn_RateCardHelp = 3355;
        public const int Btn_RateCardApprove = 3356;
        public const int Btn_RateCardDelete = 3382;

        public const int Mnu_Quotation = 3357;
        public const int Tab_Quotation = 3358;
        public const int Tab_QuotationForApproval = 3359;
        public const int Btn_QuotationAdd = 3360;
        public const int Btn_QuotationEdit = 3361;
        public const int Btn_QuotationView = 3362;
        public const int Btn_QuotationSave = 3363;
        public const int Btn_QuotationReset = 3364;
        public const int Btn_QuotationReport = 3365;
        public const int Btn_QuotationClose = 3366;
        public const int Btn_QuotationHelp = 3367;
        public const int Btn_QuotationApprove = 3368;
        public const int Btn_QuotationDelete = 3383;

        public const int Mnu_SalesOrder = 3369;
        public const int Tab_SalesOrder = 3370;
        public const int Tab_SalesOrderForApproval = 3371;
        public const int Btn_SalesOrderAdd = 3372;
        public const int Btn_SalesOrderEdit = 3373;
        public const int Btn_SalesOrderView = 3374;
        public const int Btn_SalesOrderSave = 3375;
        public const int Btn_SalesOrderReset = 3376;
        public const int Btn_SalesOrderReport = 3377;
        public const int Btn_SalesOrderClose = 3378;
        public const int Btn_SalesOrderHelp = 3379;
        public const int Btn_SalesOrderApprove = 3380;
        public const int Btn_SalesOrderDelete = 3381;

        public const int Mnu_AOD = 3384;
        public const int Tab_AOD = 3385;
        public const int Tab_AODForApproval = 3386;
        public const int Btn_AODAdd = 3387;
        public const int Btn_AODEdit = 3388;
        public const int Btn_AODView = 3389;
        public const int Btn_AODSave = 3390;
        public const int Btn_AODReset = 3391;
        public const int Btn_AODReport = 3392;
        public const int Btn_AODClose = 3393;
        public const int Btn_AODHelp = 3394;
        public const int Btn_AODApprove = 3395;
        public const int Btn_AODDelete = 3396;

        // CostCenter
        public const int Mnu_SetCostCenter = 3438;
        public const int Btn_SetSaveCostCenter = 3439; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetHelpCostCenter = 3441; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetCloseCostCenter = 3443; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetDeleteCostCenter = 3444; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetViewCostCenter = 3445; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetEditCostCenter = 3440; // parent1061 // mnu level3 // ModuleSetup
        public const int Btn_SetResetCostCenter = 3442; // parent1061 // mnu level3 // ModuleSetup

        public const int Btn_CustomerAdd = 3400;
        public const int Btn_CustomerApprove = 3401;
        public const int Btn_CustomerReport = 3402;
        public const int Btn_CustomerReset = 3403;
        public const int Tab_CustomerGeneralData = 3404;
        public const int Tab_CustomerContactDetail = 3405;
        public const int Tab_CustomerPaymentDetail = 3406;
        public const int Tab_CustomerBankDetail = 3407;
        public const int Tab_CustomerRegistration = 3408;
        public const int Tab_CustomerForApprove = 3409;

        public const int Mnu_SalaryBankTrans = 3451;
        public const int Btn_SalaryBankTransAdd = 3452;
        public const int Btn_SalaryBankTransSave = 3453;
        public const int Btn_SalaryBankTransReset = 3454;
        public const int Btn_SalaryBankTransReport = 3455;

        public const int Mnu_OperationTypeAdd = 0;
        public const int Btn_OperationTypeAdd = 6459;
        public const int Btn_OperationTypeEdit = 6460;
        public const int Btn_OperationTypeView = 6461;
        public const int Btn_OperationTypeSave = 6462;
        public const int Btn_OperationTypeApprove = 6463;
        public const int Btn_OperationTypeDelete = 6464;
        public const int Btn_OperationTypeReset = 6465;
        public const int Btn_OperationTypeReport = 6466;
        public const int Btn_OperationTypeClose = 6467;

        public const int Btn_SetAddInspection = 0;
        public const int Btn_SetEditInspection = 0;
        public const int Btn_SetSaveInspection = 0;
        public const int Btn_SetDeleteInspection = 0;
        public const int Btn_SetViewInspection = 0;
        public const int Btn_SetApprovalInspection = 0;


        public const int Btn_SetAddRepairJob = 0;
        public const int Btn_SetEditRepairJob = 0;
        public const int Btn_SetSaveRepairJob = 0;
        public const int Btn_SetDeleteRepairJob = 0;
        public const int Btn_SetViewRepairJob = 0;
        public const int Btn_SetApprovalRepairJob = 0;

        public const int Btn_SetAddDamage = 0;
        public const int Btn_SetEditDamage = 0;
        public const int Btn_SetSaveDamage = 0;
        public const int Btn_SetDeleteDamage = 0;
        public const int Btn_SetViewDamage = 0;
        public const int Btn_SetApprovalDamage = 0;

        public const int Btn_SetAddFinalInspection = 0;
        public const int Btn_SetEditFinalInspection = 0;
        public const int Btn_SetSaveFinalInspection = 0;
        public const int Btn_SetDeleteFinalInspection = 0;
        public const int Btn_SetViewFinalInspection = 0;
        public const int Btn_SetApprovalFinalInspection = 0;

    }



}

