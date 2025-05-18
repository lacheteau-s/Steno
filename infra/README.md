# Steno - infra

## Requirements

| Component | Version |
|-----------|---------|
| Terraform | 1.11.4 |
| Azure CLI | 2.71.0 |
| Azure subscription | N/A |

## Files

* `providers.tf`: Terraform providers configuration.
* `main.tf`: configuration shared across to the entire project.
* `api.tf`: configuration specific to the .NET API.
* `web.tf`: configuration specific to the Vue application.
* `variables.tf`: dynamic or sensitive values to be injected at build time.

###

## Getting started

1. Log into Azure:

```sh
az login
```

2. Set the account:

```sh
az account set --subscription <subscription-id>
```

3. Create a service principal:

```sh
az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/<subscription-id>" --name="<display-name>"
```

:warning: This is a one-time operation. Once created, use the credentials provided in the output of the preceding command as shown in the next step.

4. Set environment variables:

```sh
export ARM_CLIENT_ID="<app-id>"
export ARM_CLIENT_SECRET="<password>"
export ARM_SUBSCRIPTION_ID="<subscription-id>"
export ARM_TENANT_ID="<tenant-id>"
```

5. Initialize Terraform configuration:

```sh
terraform init
```

6. See infrastructure changes:

```sh
terraform plan
```

7. Apply changes:

```sh
terraform apply
```