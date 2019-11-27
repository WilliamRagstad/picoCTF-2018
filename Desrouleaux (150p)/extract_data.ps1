$json = Get-Content "incidents.json" | ConvertFrom-Json;

$commonSourceIp = @{};
$uniqueDestination = @{};
$sentFiles = @{};
$sentFilesTot = 0;

$trackIpForUnique = "210.205.230.140";

for ($i = 0; $i -lt $json.tickets.Count; $i++) {
    $ip = $json.tickets[$i].src_ip;
    $file = $json.tickets[$i].file_hash;

    if (!$commonSourceIp.ContainsKey($ip)) { $commonSourceIp.Add($ip, 1); }
    else { $commonSourceIp[$ip] += 1; }

    if ($ip -eq $trackIpForUnique -and (-not $uniqueDestination.ContainsKey($json.tickets[$i].dst_ip))) { $uniqueDestination.Add($json.tickets[$i].dst_ip, 1); }
    elseif ($ip -eq $trackIpForUnique) { $uniqueDestination[$json.tickets[$i].dst_ip] += 1; }
    
    if (!$sentFiles.ContainsKey($file)) { $sentFiles.Add($file, 1); $sentFilesTot ++; }
    else { $sentFiles[$file] += 1; $sentFilesTot ++;; }
}

$mostCommonIp = 0;
foreach($ip in $commonSourceIp.Keys) {
    if ($mostCommonIp  -eq 0) { $mostCommonIp = $ip; }
    if ($commonSourceIp[$mostCommonIp] -lt $commonSourceIp[$ip]) { $mostCommonIp = $ip; }
}


"the most common source IP address: " + $mostCommonIp
"----"
"unique destination: " + $uniqueDestination.Count;
"----"
"unique file sent from destination: "
$sentFiles;
"Files: " + $sentFiles.Count;
"Tot: " + $sentFilesTot;
"Avrg: " + ($sentFilesTot / $sentFiles.Count)