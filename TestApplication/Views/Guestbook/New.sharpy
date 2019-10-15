{master file="~/Views/Shared/Master.sharpy" title="Add new entry"}
{* Smarty *}

<form action="/Guestbook/Add" method="post">
<table border="1">

	{if $error ne null}
		<tr>
			<td bgcolor="yellow" colspan="2">
				{if $error eq "name_empty"}You must supply a name.
				{elseif $error eq "comment_empty"} You must supply a comment.
				{/if}
			</td>
		</tr>
	{/if}
	<tr>
		<td>Name:</td>
		<td><input type="text" name="Name" value="{$name|escape}" size="40"></td>
	</tr>
	<tr>
		<td valign="top">Comment:</td>
		<td><textarea name="Comment" cols="40" rows="10">{$comment|escape}</textarea></td>
	</tr>
	<tr>
		<td colspan="2" align="center"><input type="submit" value="Submit"></td>
	</tr>

</table>
</form>