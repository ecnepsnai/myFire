   M S B A M L   `   `   `  ����
  MyFire    MyFire.TweetDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5   �   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6�   -   5        $��TweetDialog��5   ;     � Title$  Create New Post��6    � WindowStyle$ SingleBorderWindow=�6    � WindowStartupLocation$ CenterScreen=�6N    � Icon$5 //MyFire;component/Resources/Retro-Fire-Ball.ico��6s   $	��250��6?  $	��500��6L   � Topmost .  $	��500��6h  $	��250��6w   J� 	AllowDrop . $	��635��6�  .��5      � -   5        $��Grid1��5      .+�5      �� .+�5      � $��44��6
   $	��Top=�6   ��5	      �� 5
       �� ImageSource$6 0/MyFire;component/Images/composetweet-header.png��6    j� TileMode$ FlipXY=�6P    j� Stretch$
 None=�6b   .+�5      �� 	 �� Orientation$	 
Horizontal=�6   $	��5,7q�6)   .+�5      �� -   5        $��comp_cog��5   V   $8��2/MyFire;component/Images/composetweet/comp_cod.png��6   
 � Cursor$

 Hand��6H    � ToolTip$# Tweet from multiple accounts.��6f    � ContextMenu 5      �� 5      -   5        $��UsersContextMenu��5      5      �� 5      $��1��6   $��Stretch=�6   ���	#FF4B4B4B$	��5,0q�6I   �� 5      -   5        $��
comp_image��5   Z   $<��6/MyFire;component/Images/composetweet/comp_picture.png��6   $

 Hand��6L   $! Add a picture to the tweet.��6l   �� 5      -   5        $��comp_mention��5   g   $<��6/MyFire;component/Images/composetweet/comp_mention.png��6   $	��5,0q�6L   $

 Hand��6Y   $ Mention a person.��6{   �� 5      -   5        $��
comp_trend��5   X   $:��4/MyFire;component/Images/composetweet/comp_trend.png��6   $

 Hand��6J   $% Get the latest trending topics.��6j   �� 5      -   5        $��comp_message��5   b   $7��1/MyFire;component/Images/composetweet/comp_dm.png��6   $	��5,0q�6G   $

 Hand��6T   $ Send a direct message.��6v   5      �� 5      $��Right=�6   $	��5,7q�6,   $	 
Horizontal=�69   .+�5      �� -	   5        $��CharLBL��5      $��Center=�6   $��0/140��6:   $��0,0,5,0q�6G   
�������$��13(�6k    J� Effect 6z   3  -System.Windows.Media.Effects.DropShadowEffect  6�      Color$ Black��6�      	Direction$ 0Q�6�      ShadowDepth$ 0Q�6�   -
   5        CharLBL_Effect��5   �   6�   5      5      �� 5      $��1��6   $��Stretch=�6   
���   �68   5      � 5      -   5        $��DragGrid��5   	   $��Stretch=�6   $��Stretch=�67   $��0,45,0,0q�6S   
�������6e   �� 5       -   5        $��TweetBox��5    L   $��Stretch=�6   $��Georgia)�6)   $��14(�6>    �� TextWrapping$
 Wrap=�6\   " �� VerticalScrollBarVisibility$
 Auto=�6p    �� 	IsEnabled . 
���   �$��0q�6�   $��5,50,5,5q�6�   � 5#      -   5        $��AttachedPanel��5#      $��38��6
   $��Bottom=�6+   $y�	Collapsed=�6F   .+�5$      �� -   5        $��
Rectangle1��5$   G   $��26��6   $
��Left=�6   $��6,6,0,0q�66   
���   �$	��Top=�6h   $	��163��6�   ���	#FF5555556�   �� 5%      -   5        $��AttachmentLabel��5%   J   $��17��6   $
��Left=�6   $��
12,10,0,11q�66   $�� ��6a   $��Center=�6i   $	��157��6�   
�������$��CharacterEllipsis=�6�   �� 5&      -   5        $��Button2��5&   `   $��Clear Attachment��6   $��26��6'   $��Right=�63   $��0,6,6,0q�6O   $	��Top=�6o   $	��109��6�   5'      � 5*      -   5        $��MessagePanel��5*      $��38��6
   $��Bottom=�6*   $y�	Collapsed=�6E   .+�5+      �� -   5        $��
Rectangle2��5+   D   $��26��6   $
��Left=�6   $��6,6,0,0q�63   
���   �$	��Top=�6j   $	��163��6�   ._�5,      � ���	#FF555555.+�5-      �� -   5        $��UserLBL��5-   K   
�������$��17��6$   $
��Left=�60   $�� ��6Z   $��CharacterEllipsis=�6b   $��Center=�6�   $��5q�6�   5.      5/      �� 50      -   5        $��Button3��50   ^   $��Cancel Message��6   $��26��6%   $��Right=�61   $��0,6,6,0q�6M   $	��Top=�6m   $	��109��6�   51      � 54      -   5        $��DropGrid��54   
   $��0,43,0,0q�6   $y�Hidden=�6,   .+�55      �� $��1q�6   
���   �._�56      �� $(��"Drop images here to attach them...��6   $��Center=�6:   $��Center=�6W   57      ��58      �� 59       �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6+   .��5:      �  � Color$ 	#FFE5E5FF��6    � Offset$ 1Q�6&   � 5;      $ White��6   $ 0Q�6"   5<      5>      5@      5A      