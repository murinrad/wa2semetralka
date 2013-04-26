<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="WindowsAzure2" generation="1" functional="0" release="0" Id="d1951e40-7a09-478a-a3a5-02c2a8d7b0c2" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="WindowsAzure2Group" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WCFServiceWebRole1:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/WindowsAzure2/WindowsAzure2Group/LB:WCFServiceWebRole1:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="DiffRequestProcessor:cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/WindowsAzure2/WindowsAzure2Group/MapDiffRequestProcessor:cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="DiffRequestProcessor:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/WindowsAzure2/WindowsAzure2Group/MapDiffRequestProcessor:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="DiffRequestProcessorInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/WindowsAzure2/WindowsAzure2Group/MapDiffRequestProcessorInstances" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRole1:cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/WindowsAzure2/WindowsAzure2Group/MapWCFServiceWebRole1:cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/WindowsAzure2/WindowsAzure2Group/MapWCFServiceWebRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRole1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/WindowsAzure2/WindowsAzure2Group/MapWCFServiceWebRole1Instances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WCFServiceWebRole1:Endpoint1">
          <toPorts>
            <inPortMoniker name="/WindowsAzure2/WindowsAzure2Group/WCFServiceWebRole1/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapDiffRequestProcessor:cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/WindowsAzure2/WindowsAzure2Group/DiffRequestProcessor/cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" />
          </setting>
        </map>
        <map name="MapDiffRequestProcessor:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/WindowsAzure2/WindowsAzure2Group/DiffRequestProcessor/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapDiffRequestProcessorInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/WindowsAzure2/WindowsAzure2Group/DiffRequestProcessorInstances" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRole1:cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/WindowsAzure2/WindowsAzure2Group/WCFServiceWebRole1/cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/WindowsAzure2/WindowsAzure2Group/WCFServiceWebRole1/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRole1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/WindowsAzure2/WindowsAzure2Group/WCFServiceWebRole1Instances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="DiffRequestProcessor" generation="1" functional="0" release="0" software="C:\Users\Rado\Documents\GitHub\wa2semetralka\WindowsAzure2\csx\Debug\roles\DiffRequestProcessor" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;DiffRequestProcessor&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;DiffRequestProcessor&quot; /&gt;&lt;r name=&quot;WCFServiceWebRole1&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="WCFServiceWebRole1.svclog" defaultAmount="[1000,1000,1000]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/WindowsAzure2/WindowsAzure2Group/DiffRequestProcessorInstances" />
            <sCSPolicyUpdateDomainMoniker name="/WindowsAzure2/WindowsAzure2Group/DiffRequestProcessorUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/WindowsAzure2/WindowsAzure2Group/DiffRequestProcessorFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="WCFServiceWebRole1" generation="1" functional="0" release="0" software="C:\Users\Rado\Documents\GitHub\wa2semetralka\WindowsAzure2\csx\Debug\roles\WCFServiceWebRole1" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="cz.ctu.fee.murinrad.azure.servicebus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WCFServiceWebRole1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;DiffRequestProcessor&quot; /&gt;&lt;r name=&quot;WCFServiceWebRole1&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="WCFServiceWebRole1.svclog" defaultAmount="[1000,1000,1000]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/WindowsAzure2/WindowsAzure2Group/WCFServiceWebRole1Instances" />
            <sCSPolicyUpdateDomainMoniker name="/WindowsAzure2/WindowsAzure2Group/WCFServiceWebRole1UpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/WindowsAzure2/WindowsAzure2Group/WCFServiceWebRole1FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WCFServiceWebRole1UpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="DiffRequestProcessorUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="DiffRequestProcessorFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WCFServiceWebRole1FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="DiffRequestProcessorInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WCFServiceWebRole1Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="09d1c9d5-5a9f-41d8-baca-edaf77bc324f" ref="Microsoft.RedDog.Contract\ServiceContract\WindowsAzure2Contract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="56b9fcc0-378e-4b58-86c1-0389fea1f05f" ref="Microsoft.RedDog.Contract\Interface\WCFServiceWebRole1:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/WindowsAzure2/WindowsAzure2Group/WCFServiceWebRole1:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>