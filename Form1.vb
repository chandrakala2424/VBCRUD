'Imports System.Data.SqlClient

'Public Class Form1

'    Dim connectionString As String = "Data Source=.;Initial Catalog=Samples;Integrated Security=True"

'    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        DisplayData()
'    End Sub

'    Private Sub DisplayData()

'        Dim con As New SqlConnection(connectionString)
'        con.Open()

'        Dim da As New SqlDataAdapter("SELECT * FROM Employee", con)

'        Dim dt As New DataTable()

'        da.Fill(dt)

'        dgvEmployee.DataSource = dt

'        con.Close()

'    End Sub


'    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
'        'If txtname.Text = "" Or txtdept.Text = "" Or txtcity.Text = "" Then
'        '    MessageBox.Show("Please enter all fields")
'        '    Exit Sub
'        'End If

'        If txtname.Text = "" Then
'            MessageBox.Show("Please enter Name")
'            txtname.Focus()
'            Exit Sub
'        End If

'        If txtdept.Text = "" Then
'            MessageBox.Show("Please enter Department")
'            txtdept.Focus()
'            Exit Sub
'        End If

'        If txtcity.Text = "" Then
'            MessageBox.Show("Please enter City")
'            txtcity.Focus()
'            Exit Sub
'        End If

'        Dim con As New SqlConnection(connectionString)

'        Try
'            con.Open()

'            Dim cmd As New SqlCommand("INSERT INTO Employee(Name,Department,City) VALUES(@Name,@Department,@City)", con)

'            cmd.Parameters.AddWithValue("@Name", txtname.Text)
'            cmd.Parameters.AddWithValue("@Department", txtdept.Text)
'            cmd.Parameters.AddWithValue("@City", txtcity.Text)

'            cmd.ExecuteNonQuery()

'            MessageBox.Show("Record Saved Successfully")

'            DisplayData()

'            txtname.Clear()
'            txtdept.Clear()
'            txtcity.Clear()
'            txtname.Focus()

'            Dim frm As New Form2()
'            frm.Show()

'            Me.Hide()

'        Catch ex As Exception
'            MessageBox.Show(ex.Message)

'        Finally
'            con.Close()
'        End Try
'    End Sub
'End Class

Imports System.Data.SqlClient

Public Class Form1

    Dim connectionString As String = "Data Source=.;Initial Catalog=Samples;Integrated Security=True"

    Dim EmpId As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayData()
    End Sub

    Private Sub DisplayData()

        Dim con As New SqlConnection(connectionString)

        con.Open()

        Dim da As New SqlDataAdapter("SELECT * FROM Employee", con)

        Dim dt As New DataTable()

        da.Fill(dt)

        dgvEmployee.DataSource = dt

        con.Close()

        'Add Edit Button

        If dgvEmployee.Columns("Edit") Is Nothing Then

            Dim btnEdit As New DataGridViewButtonColumn()

            btnEdit.Name = "Edit"
            btnEdit.HeaderText = "Edit"
            btnEdit.Text = "Edit"
            btnEdit.UseColumnTextForButtonValue = True

            dgvEmployee.Columns.Add(btnEdit)

        End If


        'Add Delete Button

        If dgvEmployee.Columns("Delete") Is Nothing Then

            Dim btnDelete As New DataGridViewButtonColumn()

            btnDelete.Name = "Delete"
            btnDelete.HeaderText = "Delete"
            btnDelete.Text = "Delete"
            btnDelete.UseColumnTextForButtonValue = True

            dgvEmployee.Columns.Add(btnDelete)

        End If

    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If txtname.Text = "" Then
            MessageBox.Show("Please enter Name")
            txtname.Focus()
            Exit Sub
        End If

        If txtdept.Text = "" Then
            MessageBox.Show("Please enter Department")
            txtdept.Focus()
            Exit Sub
        End If

        If txtcity.Text = "" Then
            MessageBox.Show("Please enter City")
            txtcity.Focus()
            Exit Sub
        End If

        Dim con As New SqlConnection(connectionString)

        Try

            con.Open()

            Dim cmd As SqlCommand

            'UPDATE

            If Button1.Text = "Update" Then

                cmd = New SqlCommand("UPDATE Employee SET Name=@Name,Department=@Department,City=@City WHERE Id=@Id", con)

                cmd.Parameters.AddWithValue("@Id", EmpId)

            Else

                'INSERT

                cmd = New SqlCommand("INSERT INTO Employee(Name,Department,City) VALUES(@Name,@Department,@City)", con)

            End If


            cmd.Parameters.AddWithValue("@Name", txtname.Text)
            cmd.Parameters.AddWithValue("@Department", txtdept.Text)
            cmd.Parameters.AddWithValue("@City", txtcity.Text)

            cmd.ExecuteNonQuery()

            If Button1.Text = "Update" Then
                MessageBox.Show("Record Updated Successfully")
                Button1.Text = "Save"
            Else
                MessageBox.Show("Record Saved Successfully")
            End If

            DisplayData()

            txtname.Clear()
            txtdept.Clear()
            txtcity.Clear()

            txtname.Focus()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        Finally

            con.Close()

        End Try

    End Sub

    'GRID CLICK EVENT

    Private Sub dgvEmployee_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEmployee.CellClick

        If e.RowIndex >= 0 Then

            'Edit Button

            If dgvEmployee.Columns(e.ColumnIndex).Name = "Edit" Then

                EmpId = Convert.ToInt32(dgvEmployee.Rows(e.RowIndex).Cells("Id").Value)

                txtname.Text = dgvEmployee.Rows(e.RowIndex).Cells("Name").Value.ToString()

                txtdept.Text = dgvEmployee.Rows(e.RowIndex).Cells("Department").Value.ToString()

                txtcity.Text = dgvEmployee.Rows(e.RowIndex).Cells("City").Value.ToString()

                Button1.Text = "Update"

            End If


            'Delete Button

            If dgvEmployee.Columns(e.ColumnIndex).Name = "Delete" Then

                If MessageBox.Show("Are you sure you want to delete this record?",
                                   "Confirm",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question) = DialogResult.Yes Then

                    EmpId = Convert.ToInt32(dgvEmployee.Rows(e.RowIndex).Cells("Id").Value)

                    DeleteEmployee()

                End If

            End If

        End If

    End Sub



    'DELETE METHOD

    Private Sub DeleteEmployee()

        Dim con As New SqlConnection(connectionString)

        Try

            con.Open()

            Dim cmd As New SqlCommand("DELETE FROM Employee WHERE Id=@Id", con)

            cmd.Parameters.AddWithValue("@Id", EmpId)

            cmd.ExecuteNonQuery()

            MessageBox.Show("Record Deleted Successfully")

            DisplayData()

            ClearData()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        Finally

            con.Close()

        End Try

    End Sub


    '//Chnadrakla antheree
    'CLEAR METHOD

    Private Sub ClearData()

        txtname.Clear()
        txtdept.Clear()
        txtcity.Clear()

        txtname.Focus()

        EmpId = 0

        Button1.Text = "Save"

    End Sub

End Class