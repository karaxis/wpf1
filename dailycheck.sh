#!/bin/bash

software = $1

/PATH/TO/EXECUTEABLE software

echo "MESSAGE BODY HERE" | mail -s "daily CVE check for $software" recipient@example.com -A /PATH/TO/EXECUTABLE/$software 