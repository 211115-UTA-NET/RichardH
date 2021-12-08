#!/usr/bin/bash

#Prompt the user
echo "Enter the filename you would like to add. Include the file extension."

#accept user input
read name


#Filter only .txt, .md, .sh, .png files


save the extention abreviation as ext
ext="${name##*.}"

if (( $ext == "txt" || $ext == "md" || $ext == "sh" || $ext == "png" ))
then 
	#add the entered file to staging
	git add $name

	#Prompt user for commit message
	echo "please enter the commit message"

	#Accept user input
	read message
	
	#commit the staged files
	git commit -m "$message"

	#push the commit
	git push

else
	echo "that file will not be accepeted"
fi


