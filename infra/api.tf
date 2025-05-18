resource "azurerm_service_plan" "steno_api" {
  depends_on = [ azurerm_resource_group.steno ]

  name = "svcplan-${var.project_name}-api"
  resource_group_name = azurerm_resource_group.steno.name
  location = azurerm_resource_group.steno.location
  os_type = "Linux"
  sku_name = "F1"
}

resource "azurerm_linux_web_app" "steno-api" {
  depends_on = [ azurerm_resource_group.steno, azurerm_service_plan.steno_api ]

  name = "appsvc-${var.project_name}-api"
  resource_group_name = azurerm_resource_group.steno.name
  location = azurerm_resource_group.steno.location
  service_plan_id = azurerm_service_plan.steno_api.id
  https_only = true

  site_config {}
}