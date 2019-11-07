﻿{master file="/Shared/Master.sharpy" title="Guestbook"}
{* Smarty *}

<table border="0" bgcolor="#eeeeee" width="300">
	<tr>
		<th colspan="2" bgcolor="#d1d1d1">Guestbook Entries (<a href="/Guestbook/New">add</a>)</th>
	</tr>
	{foreach from=$Model item="entry"}
		<tr>
			<td>{$entry.Name|escape}</td>        
			<td align="right">{$entry.Date|date_format:"dd/MMM/yyyy"}</td>        
		</tr>
		<tr>
			<td colspan="2" bgcolor="#dedede">{$entry.Comment|escape}</td>
		</tr>
	{foreachelse}
		<tr>
			<td colspan="2">No records</td>
		</tr>
	{/foreach}


</table>


<p>Resource: {"/lib/js/app.js"|content}</p>
<p>DateFormat: {$date|date_format:"yyyy-MM-dd HH:mm:ss"}</p>

{include file="Guestbook/I2.tpl"}

<p>{$uuid}</p>