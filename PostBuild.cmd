@echo on
:: PostBuild.cmd <Configuration> <OutPath>
:: $(SolutionDir)\PostBuild.cmd $(ConfigurationName) "$(TargetDir)$(TargetFileName)"
pushd %~dp0
copy /y "%~2" "%~1\Plugin"
popd