.model SMALL
.stack 100h
.data
		passwordToWrite DB 'alinur18'
		newLine DB 13, 10, '$'
		colon DB ':$'
		space DB ' $'
		password DB 8 DUP(?), 0
		path DB 'password.txt', 0
		errorMessage DB 'An erorr has occured.', '$'
		passwordFailMessage DB 'Incorrect password.', '$'
		welcomeMessage DB 'Password: ', '$'
		buffer DB 20 DUP(' '), '$'
		daysOfMonth DB 'January$  ', 'February$ ', 'March$    ', \
		'April$    ', 'May$      ', 'June$     ', 'July$     ', 'August$   ', 'September$', \
		'October$  ', 'November$ ', 'December$ '
.code	
		; Utility macro to write text to the console.
		writeMessage MACRO text
			mov DX, OFFSET text
			mov AH, 9h
			int 21h
		ENDM
		
		startupcode
		; You may delete these 4 lines of code below as well as the 'passwordToWrite' variable
		; if you've already created a file specified in the 'path' variable
		push OFFSET passwordToWrite
		push OFFSET path
		push 8
		call SaveMessage
		
		push OFFSET password
		push 8h
		push OFFSET path
		call ReadMessage
		
		mov BX, 0
start:	cmp BX, 3h
		je fail
		
		writeMessage welcomeMessage
		push 8
		mov DI, OFFSET buffer
		call UserInput
		writeMessage newLine
		
		mov SI, OFFSET password
		mov DI, OFFSET buffer
		push 8h
		call CompareStrings
		
		inc BX
		cmp DX, 0h
		jne start
		
		call PrintDate
		writeMessage newLine
		call PrintTime
		; dos interrupt to end the programm.
		mov AH, 4ch
		int 21h
		
fail:	writeMessage passwordFailMessage
		mov AH, 4ch
		int 21h
		
		; Procedure that writes current date and time to the console.
		PrintDate PROC
				mov AH, 2ah
				int 21h
				push CX
				push DX
				
				xor AX, AX
				mov AL, DH
				mov BL, 10
				mul BL
				
				mov SI, OFFSET daysOfMonth
				add SI, AX
				mov AH, 9h
				mov DX, SI
				int 21h
				writeMessage space
				
				pop DX
				xor DH, DH
				mov AX, DX
				call PrintNum
				writeMessage space
				
				pop CX
				mov AX, CX
				call PrintNum
				ret
		PrintDate ENDP
		
		; Procedure for displaying current time.
		PrintTime PROC
				mov	AH, 2ch
				int	21h
				push DX
				push CX

				xor AX, AX
				mov AL, CH
				call PrintNum
				writeMessage colon

				pop CX
				xor AX, AX
				mov AL, CL
				call PrintNum
				writeMessage colon

				pop DX
				xor AX, AX
				mov AL, DH
				call PrintNum
				ret
		PrintTime ENDP
		
		; Converts a number to its composite digits.
		; Takes in: one parameter - num (number to convert).
		; Returns: the number's length at the top of the stack, followed by
		; the number's digits (723 -> 3 3 2 7).
		dectoascii MACRO num
				mov CX, num
				xor DI, DI
				mov BX, 10
		@@1:
				inc DI
				xor DX, DX
				mov AX, CX
				div BX
				push DX
				mov CX, AX
				cmp CX, 0
				je	@@2  
				jmp @@1
		@@2:
				push DI
		ENDM
		
		; Prints a number contained within the stack.
		; Requires: a number to be contained within the AX register.
		; Does: displays the number to the console window.
		PrintNum PROC
				dectoascii AX
				; CX now contains the number's length
				pop CX
		@@3:	
				mov AH, 2h
				; Getting the last digit of the number
				pop DX
				; Coverting the raw number to its string representation in ASCII
				add DL, '0'
				int 21h
				loop @@3
				ret
		PrintNum ENDP
		
		; Procedure for comparing two strings for equality
		; Requires: the length up to which to compare on the stack, and
		; SI and DI registers to point to the strings.
		; Returns: DX register set to 1 if the strings aren't the same, 0 otherwise.
		len EQU [BP + 4]
		CompareStrings PROC
				push BP
				mov BP, SP
				push BX
				push AX
				
				mov DX, 0
				mov BX, 0
		do:		cmp len, BX
				je pass
				mov AL, [SI + BX]
				mov AH, [DI + BX]
				add BX, 1
				cmp AL, AH
				je do
				mov DX, 1
				
		pass:	pop AX
				pop BX
				pop BP
				ret 2
		CompareStrings ENDP
		
		
		; Procdeure to input string data from the user.
		; Requires: the maximum length of input on the stack, and
		; DI register set to point to the buffer.
		; Returns: CX register set to te length of the input
		maxLen EQU [BP + 4]
		UserInput PROC
				push BP
				mov BP, SP
				push AX
				push DX
				
				mov CX, 0
		passLoop:
				; dos interrupt for character input with echo
				; Returns: character read in the AL register
				mov AH, 8h		
				int 21h
				cmp AL, 0dh
				je	endLoop
				cmp CX, maxLen
				; in case we run out of allowed length, wait for 'Enter'
				je 	passLoop
				; assigning the typed in symbol to the next position
				mov [DI], AL
				inc	DI
				inc CX     
				; dos interrupt to write character to standard output.
				; since it's the password we're typing, display it with asteriscs.
				mov AH, 2h
				mov	DX, CX
				add DL, '0'
				mov AH, 2h
				mov	DL, '*'
				int	21h				
				jmp passLoop
		endLoop:
				pop DX
				pop AX
				pop BP
				ret 2
		UserInput ENDP
		
		; Procedure to write a text to a file.
		; Requires parameters on the stack: 1st: length of the message, 2nd: pointer to the file path,
		; 3rd: pointer to the message itself. All in the defined order starting from top to bottom.
		sCharsToWrite EQU [BP + 4]
		sFilePath EQU [BP + 6]
		sTextToWrite EQU [BP + 8]
		sFileHandle EQU [BP - 2]
		sBytesForLocals EQU 2
		SaveMessage PROC
				push BP
				mov BP, SP
				sub SP, sBytesForLocals
				
				; dos interrupt to create or truncate a file.
				mov AH, 3ch
				mov CX, 0
				mov DX, sFilePath
				int 21h
				jc	error
				mov sFileHandle, AX
				
				; dos interrupt used for writing to a file.
				mov AH, 40h
				mov BX, sFileHandle
				mov CX, sCharsToWrite
				mov DX, sTextToWrite
				int 21h
				jc	error
							
				; dos interrupt to close a file.
				mov AH, 3eh
				mov BX, sFileHandle
				int 21h
				jc	error
				
				add SP, sBytesForLocals
				pop BP
				ret 6

		error:	writeMessage errorMessage
				pop BP
				ret 6
		SaveMessage ENDP
		
		
		; Procedure to read from a file.
		; Requires parameters on the stack: 1st: pointer to the file path, 2nd: length of the file,
		; 3rd: pointer to the buffer. All in the defined order starting from top to bottom.
		; Returns: buffer filled with the file's entrails
		rFilePath EQU [BP + 4]
		rReadBytes EQU [BP + 6]
		rBuffer EQU [BP + 8]
		rFileHandle EQU [BP - 2]
		rBytesForLocals EQU 2
		ReadMessage PROC
				push BP
				mov BP, SP
				sub SP, rBytesForLocals
				
				; dos interrupt to open an existing file.
				mov AH, 3dh
				mov AL, 0
				mov DX, rFilePath
				int 21h
				jc  fault
				mov rFileHandle, AX
				
				; dos interrupt to read from a file.
				mov	AH, 3fh
				mov CX, rReadBytes
				mov BX, rFileHandle
				mov DX, rBuffer
				int 21h
				jc	fault
				
				mov AH, 3eh
				mov BX, rFileHandle
				int 21h
				jc fault
				
				pop BP
				add SP, rBytesForLocals
				ret 6
		
		fault:  writeMessage errorMessage
				pop BP
				add SP, rBytesForLocals
				ret 6
		ReadMessage ENDP
				
end