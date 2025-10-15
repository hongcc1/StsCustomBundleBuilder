IF EXIST "TesterSettings" (
    RD /S /Q "TesterSettings"
)
MD "TesterSettings"

MD "TesterSettings\AppData"
MD "TesterSettings\AppData\Cfg"

COPY "%teststandappdata64%\Cfg\Users.ini" "TesterSettings\AppData\Cfg"

MD "TesterSettings\AppData\Cfg\ModelPlugins"
COPY "%teststandappdata64%\Cfg\ModelPlugins\ResultProcessing.cfg" "TesterSettings\AppData\Cfg\ModelPlugins"

MD "TesterSettings\AppData\Cfg\NI_SemiconductorModule"
COPY "%teststandappdata64%\Cfg\NI_SemiconductorModule\HandlerProberDrivers.cfg" "TesterSettings\AppData\Cfg\NI_SemiconductorModule"
COPY "%teststandappdata64%\Cfg\NI_SemiconductorModule\StationSettings.cfg" "TesterSettings\AppData\Cfg\NI_SemiconductorModule"



