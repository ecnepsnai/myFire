   M S B A M L   `   `   `  ���� 
  MyFire    MyFire.TweetDialog   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   $��TweetDialog��  � Title$  Create New Post�� � WindowStyle$ SingleBorderWindow=� � WindowStartupLocation$ CenterScreen=� � Icon$5 //MyFire;component/Resources/Retro-Fire-Ball.ico��$	��250��$	��500�� � Topmost .  $	��500��$	��250�� J� 	AllowDrop . $	��635��.��� -   $��Grid1��.+��� .+�� $��44��$	��Top=�����  �� ImageSource$6 0/MyFire;component/Images/composetweet-header.png�� j� TileMode$ FlipXY=� j� Stretch$
 None=�.+��� 	 �� Orientation$	 
Horizontal=�$	��5,7q�.+��� -   $��comp_cog��$8��2/MyFire;component/Images/composetweet/comp_cod.png��
 � Cursor$

 Hand�� � ToolTip$# Tweet from multiple accounts.�� � ContextMenu �� -   $��UsersContextMenu���� $��1��$��Stretch=����	#FF4B4B4B$	��5,0q��� -   $��
comp_image��$<��6/MyFire;component/Images/composetweet/comp_picture.png��$

 Hand��$! Add a picture to the tweet.���� -   $��comp_mention��$<��6/MyFire;component/Images/composetweet/comp_mention.png��$	��5,0q�$

 Hand��$ Mention a person.���� -   $��
comp_trend��$:��4/MyFire;component/Images/composetweet/comp_trend.png��$

 Hand��$% Get the latest trending topics.���� -   $��comp_message��$7��1/MyFire;component/Images/composetweet/comp_dm.png��$	��5,0q�$

 Hand��$ Send a direct message.���� $��Right=�$	��5,7q�$	 
Horizontal=�.+��� -	   $��CharLBL��$��Center=�$��0/140��$��0,0,5,0q�
�������$��13(� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   	Direction$ 0Q�   ShadowDepth$ 0Q�-
   CharLBL_Effect���� $��1��$��Stretch=�
���   �� -   $��DragGrid��$��Stretch=�$��Stretch=�$��0,45,0,0q�
��������� -   $��TweetBox��$��Stretch=�$��Georgia)�$��14(� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$
 Auto=� �� 	IsEnabled . 
���   �$��0q�$��5,50,5,5q�� -   $��AttachedPanel��$��38��$��Bottom=�$y�	Collapsed=�.+��� -   $��
Rectangle1��$��26��$
��Left=�$��6,6,0,0q�
���   �$	��Top=�$	��163�����	#FF555555�� -   $��AttachmentLabel��$��17��$
��Left=�$��
12,10,0,11q�$�� ��$��Center=�$	��157��
�������$��CharacterEllipsis=��� -   $��Button2��$��Clear Attachment��$��26��$��Right=�$��0,6,6,0q�$	��Top=�$	��109��� -   $��MessagePanel��$��38��$��Bottom=�$y�	Collapsed=�.+��� -   $��
Rectangle2��$��26��$
��Left=�$��6,6,0,0q�
���   �$	��Top=�$	��163��._�� ���	#FF555555.+��� -   $��UserLBL��
�������$��17��$
��Left=�$�� ��$��CharacterEllipsis=�$��Center=�$��5q��� -   $��Button3��$��Cancel Message��$��26��$��Right=�$��0,6,6,0q�$	��Top=�$	��109��� -   $��DropGrid��$��0,43,0,0q�$y�Hidden=�.+��� $��1q�
���   �._��� $(��"Drop images here to attach them...��$��Center=�$��Center=�����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFE5E5FF�� � Offset$ 1Q�� $ White��$ 0Q�