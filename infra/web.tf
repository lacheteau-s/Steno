resource "azurerm_static_web_app" "steno_web" {
  depends_on = [ azurerm_resource_group.steno ]

  name = "webapp-${var.project_name}-web"
  resource_group_name = azurerm_resource_group.steno.name
  location = azurerm_resource_group.steno.location

  sku_tier = "Free"
  sku_size = "Free"
}