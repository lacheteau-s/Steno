# Steno - infra

## Requirements

* Terraform 1.11.x
* Azure subscription
* Azure CLI

## Getting started

1. Log into Azure

```sh
az login
```

2. Set the account:

```sh
az account set --subscription <subscription-id>
```

3. Create a service principal

```sh
az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/<subscription-id>"
```

4. Set the environment variables using the output from the previous command

```sh
export ARM_CLIENT_ID="<app-id>"
export ARM_CLIENT_SECRET="<password>"
export ARM_CLIENT_SUBSCRIPTION="<subscription-id>"
export ARM_TENANT_ID="<tenant>"
```

5. Initialize Terraform

```sh
terraform init
```

6. Apply the configuration

```sh
terraform apply
```