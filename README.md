# k8s-dotnet-memleak-analyzer

This project demonstrates how to analyze memory leaks in .NET applications running in a Kubernetes cluster using the `dotnet-dump` diagnostic tool. It provides a practical example of collecting and analyzing memory dumps in a containerized environment.

## Overview

The setup (see `memory-leak-analyze.yaml`) consists of two containers running in a single pod:

1. **memoryleakapiapp**: The main .NET application container that may experience memory leaks
2. **dot-dump**: A sidecar container with diagnostic tools for collecting and analyzing memory dumps

The containers share:

- Process namespace (allowing the diagnostic container to see the main app's processes)
- Volume mounts for storing and accessing memory dumps

## Prerequisites

- Kubernetes cluster with kubectl configured
- Basic understanding of Kubernetes and .NET diagnostics

## Usage

1. Deploy the application to your Kubernetes cluster:

```bash
kubectl apply -f memory-leak-analyze.yaml
```

2. Access the diagnostic container:

```bash
kubectl exec -it pods/memoryleakapi-[POD_ID] -c dot-dump -- bash
```

3. Collect and analyze memory dumps:

```bash
# Collect memory dump
dotnet-dump collect -p $(pidof dotnet) --output /dumps/mycoredump

# Analyze the dump file
dotnet-dump analyze /dumps/mycoredump
```

## Key Features

- Shared process namespace enabling cross-container diagnostics
- Persistent volume for dump storage
- Non-intrusive memory analysis
- Real-time debugging capabilities

## Notes

- The dot-dump container requires privileged access for memory dump collection
- Dumps are stored in an emptyDir volume, which is ephemeral and tied to the pod lifecycle
- Replace [POD_ID] with your actual pod identifier in the exec command
