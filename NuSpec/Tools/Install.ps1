param($installPath, $toolsPath, $package, $project)

$DTE.ItemOperations.Navigate("http://encel.co/download/install/?" + $package.Id + "=" + $package.Version)