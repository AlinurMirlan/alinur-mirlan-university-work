USE University
GO

ALTER TABLE �����
	ADD CONSTRAINT FK_������_�������_�����
		FOREIGN KEY (�������������) REFERENCES ������_�������(���������);

ALTER TABLE ����� 
	ADD CONSTRAINT FK_����_�����_�����
		FOREIGN KEY (��������) REFERENCES �����(��������);

ALTER TABLE �����
	ADD CONSTRAINT FK_����������_�����
		FOREIGN KEY (�������������) REFERENCES ����������(�������������);

ALTER TABLE �����
	ADD CONSTRAINT FK_����_������������_�����
		FOREIGN KEY (��������������) REFERENCES ����_������������(�������������);

ALTER TABLE �����
	ADD CONSTRAINT FK_����������_�����
		FOREIGN KEY (�������������) REFERENCES ����������(�������������);

ALTER TABLE �����
	ADD CONSTRAINT FK_����_�����_�����
		FOREIGN KEY (������������) REFERENCES ����_�����(������������);
GO