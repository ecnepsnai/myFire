Imports System
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Interop

Namespace WindowFlash
    Public Class WindowExtensions
#Region "Window Flashing API"
        Private Const FLASHW_STOP As Integer = 0
        Private Const FLASHW_CAPTION As Integer = 1
        Private Const FLASHW_TRAY As Integer = 2
        Private Const FLASHW_ALL As Integer = 3
        Private Const FLASHW_TIMER As Integer = 4
        Private Const FLASHW_TIMERNOFG As Integer = 12

        Private Structure FLASHWINFO
            Public cbSize As UInt32
            Public hwnd As IntPtr
            Public dwFlags As UInt32
            Public uCount As UInt32
            Public dwTimeout As UInt32
        End Structure

        <DllImport("user32.dll")> _
        Private Shared Function FlashWindowEx(ByRef pwfi As FLASHWINFO) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function
#End Region

        Public Shared Sub FlashWindow(win As Window, Optional count As UInt32 = UInt32.MaxValue)
            If win.IsActive Then
                Return
            End If

            Dim h As New WindowInteropHelper(win)

            Dim info As New FLASHWINFO() With { _
                .hwnd = h.Handle, _
                .dwFlags = FLASHW_ALL Or FLASHW_TIMER, _
                .uCount = count, _
                .dwTimeout = 0 _
            }

            info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info))
            FlashWindowEx(info)
        End Sub

        Public Shared Sub StopFlashingWindow(win As Window)
            Dim h As New WindowInteropHelper(win)
            Dim info As New FLASHWINFO()
            info.hwnd = h.Handle
            info.cbSize = Convert.ToUInt32(Marshal.SizeOf(info))
            info.dwFlags = FLASHW_STOP
            info.uCount = UInt32.MaxValue
            info.dwTimeout = 0
            FlashWindowEx(info)
        End Sub
    End Class
End Namespace