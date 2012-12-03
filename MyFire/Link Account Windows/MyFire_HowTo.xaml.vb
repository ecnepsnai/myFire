'* 
'* myFire
'*
'* This file is part of the myFire software
'* Copyright (c) 2011, Ian Spence
'* All rights reserved.
'*
'* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
'* ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
'* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
'* IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
'* INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
'* NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
'* PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
'* WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
'* ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
'* POSSIBILITY OF SUCH DAMAGE.
'*
Imports Twitterizer
Public Class MyFire_HowTo

    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Dim t As IList(Of Tweet)
        t = New List(Of Tweet)() From { _
                  New Tweet() With { _
                    .TwitterStatusObject = myfireactions.Twitter_Status_Show(135504025037705216)
                  }
                }
        TMList.Items.Add(t)
    End Sub

    Private Sub profile_ecnepsnai_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles profile_ecnepsnai.Click
        Dim up As New UserProfile
        up.Show_User("ecnepsnai")
    End Sub

    Private Sub profile_myfire_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles profile_myfire.Click
        Dim up As New UserProfile
        up.Show_User("MyFireApp")
    End Sub

    Private Sub profile_twitter_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles profile_twitter.Click
        Dim up As New UserProfile
        up.Show_User("Twitter")
    End Sub

    Private Sub follow_ecnepsnai_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles follow_ecnepsnai.Click
        myfireactions.Twitter_Friendship_Create("ecnepsnai")
    End Sub

    Private Sub follow_twinnedchimera_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles follow_twinnedchimera.Click
        myfireactions.Twitter_Friendship_Create("twinnedchimera")
    End Sub

    Private Sub follow_myfireapp_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles follow_myfireapp.Click
        myfireactions.Twitter_Friendship_Create("MyFireApp")
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        TabControl1.SelectedItem = TabItem2
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button2.Click
        TabControl1.SelectedItem = TabItem3
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button6.Click
        TabControl1.SelectedItem = TabItem4
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button3.Click
        Process.Start("http://pledgie.com/campaigns/16190")
    End Sub

    Private Sub next_6_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles next_6.Click
        TabControl1.SelectedItem = TabItem5
    End Sub

    Private Sub Add_Twitter_Account_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Add_Twitter_Account.Click
        Dim us As New myFire_UserManagment
        us.ShowDialog()
    End Sub
End Class
