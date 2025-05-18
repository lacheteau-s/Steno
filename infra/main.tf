resource "azurerm_resource_group" "steno" {
  name = "resgrp-${var.project_name}"
  location = "East US"
}