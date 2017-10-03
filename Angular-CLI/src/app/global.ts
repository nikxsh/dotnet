//Constants
export const PAGE_SIZE = 10;
export const PAGE_LENGTH = 5;
export const Product_Types = [
    { key: 0, value: "Goods" },
    { key: 1, value: "Services" }
];

//Select Base URL
//export const Domain = window.location.hostname == 'localhost' ? 'http://localhost:5757/' : 'http://www.brewbuck.com/domain/';
export const Domain = 'http://www.brewbuck.com/domain/';

//API Prefix added
const ServiceDomain = Domain + 'api/';

//Manage Account
export const API_LOGIN = ServiceDomain + 'Account/Login';
export const API_CHANGE_PASSWORD = ServiceDomain + 'User/ChangeUserPassword';
export const API_RESET_PASSWORD = ServiceDomain + 'User/ForgetUserPassword';

//Manage Users
export const API_GET_ALL_USER = ServiceDomain + 'User/GetUsersByTenantId';
export const API_ADD_USER = ServiceDomain + 'User/AddUser';
export const API_EDIT_USER = ServiceDomain + 'User/EditUser';
export const API_ENABLE_DISABLE_USER = ServiceDomain + 'User/EnableDisableUser';

//Manage Roles and Permissions
export const API_GET_TENANT_ROLES = ServiceDomain + 'Role/GetRolesByTenantId';
export const API_GET_PERMISSION_FOR_ROLE = ServiceDomain + 'Role/GetRoleById';
export const API_SAVE_ROLE = ServiceDomain + 'Role/AddRole';
export const API_SAVE_PERMISSION_FOR_ROLE = ServiceDomain + 'Role/UpdateRolePermission';
export const API_EDIT_ROLE = ServiceDomain + 'Role/EditRole';
export const API_ENABLE_DISABLE_ROLE = ServiceDomain + 'Role/EnableDisableRole';


//Manage Tenant
export const API_REGISTER_TENANT = ServiceDomain + 'Tenant/register';
export const API_GET_TENANT_PROFILE = ServiceDomain + 'Tenant/GetTenantProfileById';
export const API_SAVE_TENANT_PROFILE = ServiceDomain + 'Tenant/EditTenantProfile';
export const API_UPLOAD_TENANT_LOGO = ServiceDomain + 'Tenant/UploadTenantLogo';

//Manage Customers
export const API_GET_TENANT_CUSTOMERS_CONTACTS = ServiceDomain + 'customer/all';
export const API_GET_TENANT_VENDORS_CONTACTS = ServiceDomain + 'vendor/all';
export const API_ADD_TENANT_CUSTOMER_CONTACT = ServiceDomain + 'customer/add';
export const API_ADD_TENANT_VENDOR_CONTACT = ServiceDomain + 'vendor/add';
export const API_EDIT_TENANT_CUSTOMER_CONTACT = ServiceDomain + 'customer/edit';
export const API_EDIT_TENANT_VENDOR_CONTACT = ServiceDomain + 'vendor/edit';
export const API_ENABLE_DISABLE_CUSTOMER_CONTACT = ServiceDomain + 'customer/enable';
export const API_ENABLE_DISABLE_VENDOR_CONTACT = ServiceDomain + 'vendor/enable';
//Manage Vendors
export const API_GET_TENANT_VENDORS = ServiceDomain + 'vendor/all';
export const API_ADD_TENANT_VENDOR = ServiceDomain + 'vendor/add';
export const API_EDIT_TENANT_VENDOR = ServiceDomain + 'vendor/edit';
export const API_ENABLE_DISABLE_VENDOR = ServiceDomain + 'vendor/enable';

//Manage Product(Categories)     
export const API_ADD_PRODUCT = ServiceDomain + 'product/add';
export const API_EDIT_PRODUCT = ServiceDomain + 'product/edit';
export const API_DELETE_PRODUCT = ServiceDomain + 'product/delete';
export const API_ENABLE_DISABLE_PRODUCT = ServiceDomain + 'product/enable';

export const API_GET_ALL_PRODUCTS = ServiceDomain + 'product/all';    
export const API_GET_PRODUCTS_OF_CATEGORY = ServiceDomain + 'pcategory/products';
export const API_GET_PRODUCT_BY_ID = ServiceDomain + 'product/get';

export const API_ADD_PRODUCT_CATEGORY = ServiceDomain + 'pcategory/add';
export const API_EDIT_PRODUCT_CATEGORY = ServiceDomain + 'pcategory/edit';
export const API_DELETE_PRODUCT_CATEGORY = ServiceDomain + 'pcategory/delete';
export const API_ENABLE_DISABLE_PRODUCT_CATEGORY = ServiceDomain + 'pcategory/enable';

export const API_GET_ALL_PRODUCT_CATEGORIES = ServiceDomain + 'pcategory/all';
export const API_GET_PRODUCT_CATEGORY = ServiceDomain + 'pcategory/get';

//Manage Invoices                  
export const API_GET_INVOICE_NUMBER = ServiceDomain + 'invoice/getInvoiceId';
export const API_GET_INVOICE_PAYMENTS = ServiceDomain + 'invoice/GetAllInvoicePayments';
export const API_GET_INVOICE_BY_ID = ServiceDomain + 'invoice/get';
export const API_GET_INVOICE_ITEM_QUANTITY = ServiceDomain + 'sales/getQuantities';
export const API_GET_SALES_INVOICE_BY_ID = ServiceDomain + 'sales/get';
export const API_GET_ALL_INVOICES = ServiceDomain + 'invoice/all';
export const API_CREATE_INVOICE = ServiceDomain + 'invoice/add';
export const API_EDIT_INVOICE = ServiceDomain + 'invoice/edit';
export const API_DELETE_INVOICE = ServiceDomain + 'invoice/delete';
export const API_PAY_INVOICE_AMOUNT = ServiceDomain + 'invoice/payment/update';
export const API_INVOICE_EXPORT_TO_EXCEL= ServiceDomain + 'invoice/exporttoexcel';

//Manage Sales Order                  
export const API_GET_SALES_ORDER_NUMBER = ServiceDomain + 'sales/getSalesOrderId';
export const API_GENERATE_SALES_ORDER = ServiceDomain + 'sales/add';
export const API_GET_ALL_SALES_ORDERS = ServiceDomain + 'sales/all';
export const API_GET_SALES_ORDER_BY_ID = ServiceDomain + 'sales/get';
export const API_EDIT_SALES_ORDER = ServiceDomain + 'sales/edit';
export const API_DELETE_SALES_ORDER = ServiceDomain + 'sales/delete';
export const API_CREATE_SALES_INVOICE = ServiceDomain + 'sales/invoice/add';
export const API_EDIT_SALES_INVOICE = ServiceDomain + 'sales/invoice/edit';
export const API_DELETE_SALES_INVOICE = ServiceDomain + 'sales/invoice/delete';
export const API_PAY_SALES_INVOICE_AMOUNT = ServiceDomain + 'sales/invoice/payment/update';
// ---- API Routes- End ---- //

//Manage Bills                  
export const API_GET_BILL_NUMBER = ServiceDomain + 'bill/getBillId';
export const API_GET_BILL_PAYMENTS = ServiceDomain + 'bill/GetAllBillPayments';
export const API_GET_BILL_BY_ID = ServiceDomain + 'bill/get';
export const API_GET_ALL_BILLS = ServiceDomain + 'bill/all';
export const API_CREATE_BILL = ServiceDomain + 'bill/add';
export const API_EDIT_BILL = ServiceDomain + 'bill/edit';                              
export const API_DELETE_BILL = ServiceDomain + 'bill/delete';
export const API_PAY_BILL_AMOUNT = ServiceDomain + 'bill/payment/update';

//Purchase Order                  
export const API_GET_PURCHASEORDER_NUMBER = ServiceDomain + 'purchase/getpurchaseId';
export const API_GET_PURCHASEORDER_BY_ID = ServiceDomain + 'purchase/get';
export const API_GET_PURCHASE_RECIEVE_ITEMS = ServiceDomain + 'purchase/purchasereceive/lineitems/get';
export const API_GET_ALL_PURCHASEORDERS = ServiceDomain + 'purchase/all';
export const API_CREATE_PURCHASEORDER = ServiceDomain + 'purchase/add';
export const API_CREATE_PURCHASEORDER_BILL = ServiceDomain + 'purchase/bill/add';
export const API_EDIT_PURCHASE_RECEIVE_BILL = ServiceDomain + 'purchase/bill/update';
export const API_DELETE_PURCHASE_RECEIVE_BILL = ServiceDomain + 'purchase/bill/delete';
export const API_EDIT_PURCHASEORDER = ServiceDomain + 'purchase/edit';
export const API_DELETE_PURCHASEORDER = ServiceDomain + 'purchase/delete';
export const API_ADD_PURCHASE_RECEIVE = ServiceDomain + 'purchase/purchasereceive/add';
export const API_REMOVE_PURCHASE_RECEIVE = ServiceDomain + 'purchase/purchasereceive/delete';
export const API_PAY_PURCHASE_BILL_AMOUNT = ServiceDomain + 'purchase/bill/payment/update';

//Inventory                 
export const API_GET_ALL_INVENTORY = ServiceDomain + 'inventory/all';
export const API_GET_ALL_WIP_INVENTORY = ServiceDomain + 'inventory/wip/all';
export const API_GET_INVENTORY_HISTORY = ServiceDomain + 'inventory/history';
export const API_GET_ALL_INVENTORY_WORKFLOWS = ServiceDomain + 'inventory/workflow/all';
export const API_GET_ALL_INVENTORY_WORKFLOW_BY_ID = ServiceDomain + 'inventory/workflow/get'; 
export const API_ADD_INVENTORY_WORKFLOW = ServiceDomain + 'inventory/workflow/add';
export const API_UPDATE_INVENTORY_WORKFLOW = ServiceDomain + 'inventory/workflow/update';
export const API_DELETE_INVENTORY_WORKFLOW = ServiceDomain + 'inventory/workflow/delete';
export const API_GET_ALL_INVENTORY_WORKFLOWS_HEADS = ServiceDomain + 'inventory/workflow/name/all';
export const API_GET_ALL_INVENTORY_WORKFLOWS_WIP = ServiceDomain + 'inventory/workflow/wip/all';
export const API_ADD_INVENTORY_WORKFLOW_WIP = ServiceDomain + 'inventory/wip/add';
export const API_EDIT_INVENTORY_WORKFLOW_WIP = ServiceDomain + 'inventory/wip/update';


//Error Messages   
export const UI_ERROR = "Error Ocurred, Please try again later.";
export const UI_ADD_SUCCESS = "{0} added successfully!";
export const UI_ENABLED_DISABBLED_SUCCESS = "{0} {1} successfully!";
export const UI_EDIT_SUCCESS = "{0} updated successfully!";
export const UI_DELETE_SUCCESS = "{0} deleted successfully!";
export const UI_EMPTY_RESULT = "{0} not found!";
export const UI_DONT_TAMPER = "Do not try to tamper URL!";

