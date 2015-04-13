Imports GT = Gadgeteer
Imports GTM = Gadgeteer.Modules
Imports Gadgeteer.Modules.GHIElectronics

'FUNCTION:  Control a text display using tactile (buttons) and laser means
'           by A Caroline Dahl, April 2015
'           NASA Space Apps Challenge prototype for Team Galactic Personal Display


Namespace Click_to_updateDisplayN
    Partial Public Class Program

        Dim fontTitle As Font = Resources.GetFont(Resources.FontResources.NinaB)
        Dim fontSmall As Font = Resources.GetFont(Resources.FontResources.small)
        Dim autoUpdate_N_ms As Integer = 5000
        Dim pageN As Integer = 0
        Dim nSeconds As Integer = 0

        'After declaration, these timers need to be initialized at upstart.
        Dim WithEvents timer As GT.Timer = New GT.Timer(autoUpdate_N_ms) ' Periodicity in ms. 
        Dim WithEvents timerGetLuminance As GT.Timer = New GT.Timer(500)

        ' Hardcoded NASA manual material. Will be made into an array with auto-population option based on pure string input.
        ' From Crew Escape Systems 21002, Section 2.0: CREW-WORN EQUIPMENT (i.e. wearables)
        ' available from http://www.nasa.gov/centers/johnson/pdf/383443main_crew_escape_workbook.pdf
        Dim crew_escape_workbook_TITLE() As String = {"CREW-WORN", "ADVANCED CREW", "ACES Features"}
        Dim crew_escape_workbook_TITLE2() As String = {"EQUIPMENT", "ESCAPE SUIT", ""}
        Dim crew_escape_workbook_main() As String = {"The orbiter crewmembers", "All orbiter crewmembers", "The ACES has the"}
        Dim crew_escape_workbook_main1() As String = {"wear equipment and gear", "wear a protective suit", " following features"}
        Dim crew_escape_workbook_main2() As String = {"that facilitate quick", "during launch and entry.", "A full pressure suit,"}
        Dim crew_escape_workbook_main3() As String = {"and safe egress/escape", "The crewmembers don and", "the air pressure exerts"}
        Dim crew_escape_workbook_main4() As String = {"in an emergency occurring", "doff their suits as", "direct pressure on the"}
        Dim crew_escape_workbook_main5() As String = {"prelaunch, in flight, or", "follows: Prelaunch Launch", "body. The orange"}
        Dim crew_escape_workbook_main6() As String = {"postlanding. The crew-worn", "Postinsertion On Orbit", "outer garment of flame-"}
        Dim crew_escape_workbook_main7() As String = {"equipment and gear include", "Deorbit Prep Entry", "retardant Nomex covers"}
        Dim crew_escape_workbook_main8() As String = {"the pressure suit, helmet,", "Postlanding", "the single pressure"}
        Dim crew_escape_workbook_main9() As String = {"parachute, harness, rescue", "", "bladder."}
        Dim crew_escape_workbook_main10() As String = {"aids, and survival aids.", "", ""}

        ' This is run when the mainboard is powered up or reset. 
        Public Sub ProgramStarted()

            ' Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started")
            timerGetLuminance.Start()

            ' Initialize display
            updateDisplay(0)

            'Calibrate existing background light (not used atm)
            Dim normalLight As Double = lightSense.GetIlluminance()

        End Sub

        'Scroll by laser. If luminance > threshold, update the display.
        Private Sub retrieveLuminance(timerGetLuminance As Gadgeteer.Timer) Handles timerGetLuminance.Tick

            Dim nLight As Double = lightSense.GetIlluminance()
            Debug.Print("Luminocity is " + nLight.ToString)
            'NB Household luminance ~500-800. Laser > 900.
            'Ideally surrounding luminosity should be calibrated against, and laser intensity be a variable.
            'That way crew would be able to navigate the displayed manual in any light conditions.
            If nLight > 900 Then
                nSeconds = nSeconds + 1
                If nSeconds = 3 Then
                    nSeconds = 0
                End If
                updateDisplay(nSeconds)
            End If
        End Sub

        ' Do every N seconds:
        Private Sub timer_Tick(timer As Gadgeteer.Timer) Handles Timer.Tick
            nSeconds = nSeconds + 1

            If nSeconds = 3 Then
                nSeconds = 0
            End If

            updateDisplay(nSeconds)

        End Sub

        'Auto-scroller: start or stop
        Private Sub button_ButtonPressed(sender As Button, state As Button.ButtonState) Handles autoScrollButton.ButtonPressed
            Debug.Print("AutoScroll updateDisplay = " + nSeconds.ToString)

            'Start or Stop the auto-scroll timer
            If Timer.IsRunning Then
                Timer.Stop()
            Else
                timer.Start()
                If nSeconds = 2 Then
                    nSeconds = 0
                Else
                    nSeconds = nSeconds + 1
                End If
                updateDisplay(nSeconds)
            End If

        End Sub

        'Reset-button
        Private Sub button2_ButtonPressed(sender As Button, state As Button.ButtonState) Handles resetButton.ButtonPressed
            timer.Stop()
            If nSeconds > 0 Then
                nSeconds = 0
                updateDisplay(nSeconds)
            End If
        End Sub

        'Edit the display according to what page is sought
        Public Sub updateDisplay(nSeconds As Integer)
            Debug.Print("updateDisplay = " + nSeconds.ToString)

            ' "Clears" the screen by drawing a block across it. x, y, Width, Height
            display.SimpleGraphics.DisplayRectangle(GT.Color.Black, 2, GT.Color.Black, 0, 0, display.Width, display.Height)

            ' Title text
            display.SimpleGraphics.DisplayText(crew_escape_workbook_TITLE(nSeconds), fontTitle, GT.Color.Red, 20, 6)
            display.SimpleGraphics.DisplayText(crew_escape_workbook_TITLE2(nSeconds), fontTitle, GT.Color.Red, 20, 20)

            ' Body text
            display.SimpleGraphics.DisplayText(crew_escape_workbook_main(nSeconds), fontSmall, GT.Color.Cyan, 0, 40) 'x, y from top
            display.SimpleGraphics.DisplayText(crew_escape_workbook_main1(nSeconds), fontSmall, GT.Color.Cyan, 0, 50)
            display.SimpleGraphics.DisplayText(crew_escape_workbook_main2(nSeconds), fontSmall, GT.Color.Cyan, 0, 60)
            display.SimpleGraphics.DisplayText(crew_escape_workbook_main3(nSeconds), fontSmall, GT.Color.Cyan, 0, 70)
            display.SimpleGraphics.DisplayText(crew_escape_workbook_main4(nSeconds), fontSmall, GT.Color.Cyan, 0, 80)
            display.SimpleGraphics.DisplayText(crew_escape_workbook_main5(nSeconds), fontSmall, GT.Color.Cyan, 0, 90)
            display.SimpleGraphics.DisplayText(crew_escape_workbook_main6(nSeconds), fontSmall, GT.Color.Cyan, 0, 100)
            display.SimpleGraphics.DisplayText(crew_escape_workbook_main7(nSeconds), fontSmall, GT.Color.Cyan, 0, 110)
            display.SimpleGraphics.DisplayText(crew_escape_workbook_main8(nSeconds), fontSmall, GT.Color.Cyan, 0, 120)
            display.SimpleGraphics.DisplayText("page " + (nSeconds + 1).ToString, fontSmall, GT.Color.Cyan, 80, 140)

        End Sub
    End Class
End Namespace
