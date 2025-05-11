resource "azurerm_resource_group" "steno" {
  name     = "resgrp-${var.project_name}"
  location = "East US"
}

resource "azurerm_service_plan" "steno_web" {
  name                = "svcplan-${var.project_name}-web"
  resource_group_name = azurerm_resource_group.steno.name
  location            = azurerm_resource_group.steno.location
  os_type             = "Linux"
  sku_name            = "F1"
}

resource "azurerm_linux_web_app" "steno_web" {
  name                = "appsvc-steno-web"
  resource_group_name = azurerm_resource_group.steno.name
  location            = azurerm_service_plan.steno_web.location
  service_plan_id     = azurerm_service_plan.steno_web.id
  depends_on = [ azurerm_service_plan.steno_web ]
  https_only = true

  site_config {}
}