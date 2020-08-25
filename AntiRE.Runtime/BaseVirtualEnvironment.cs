using System;
using System.Collections.Generic;

abstract class BaseVirtualEnvironment : IVirtualEnvironment
{
    public abstract string Name { get; }

    public virtual bool ContainsDevice(IEnumerable<PnPEntity> devices)
    {
        return false;
    }

    public virtual bool ContainsDisk(IEnumerable<DiskDrive> disks)
    {
        return false;
    }

    public virtual bool ContainsProcess(IEnumerable<string> processes)
    {
        return false;
    }

    public virtual bool ContainsService(IEnumerable<WindowsService> services)
    {
        return false;
    }

    public virtual bool IsVirtual(BIOS bios)
    {
        return false;
    }

    public virtual bool IsVirtual(ComputerSystem computer)
    {
        return false;
    }

    public virtual bool Assert(ComputerSystem computer, BIOS bios, IEnumerable<DiskDrive> disks, IEnumerable<PnPEntity> devices, IEnumerable<string> processes, IEnumerable<WindowsService> services)
    {
        bool computerIsVirtual = IsVirtual(computer);
        bool biosIsVirtual = IsVirtual(bios);
        bool containsVirtualDisk = ContainsDisk(disks);
        bool containsVirtualDevice = ContainsDevice(devices);
        bool containsVirtualProcess = ContainsProcess(processes);
        bool containsVirtualService = ContainsService(services);
        return computerIsVirtual
            || biosIsVirtual
            || containsVirtualDisk
            || containsVirtualDevice
            || containsVirtualProcess
            || containsVirtualService;
    }
}